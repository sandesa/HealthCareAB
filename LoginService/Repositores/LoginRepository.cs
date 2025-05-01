using LoginService.Interfaces;
using LoginService.Models;
using LoginService.Services;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoginService.Repositores
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpClient _httpClientUser;
        private readonly HttpClient _httpClientSession;
        private readonly JwtService _jwtService;

        public LoginRepository(IHttpClientFactory factory, JwtService jwtService)
        {
            _httpClientUser = factory.CreateClient("UserService");
            _httpClientSession = factory.CreateClient("SessionService");
            _jwtService = jwtService;
        }

        public async Task<ValidationResponse?> ValidateUserAsync(LoginRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await _httpClientUser.PostAsync("validate", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                var validationResponseData = jsonResponse.GetProperty("data");
                var validationResponse = JsonSerializer.Deserialize<ValidationResponse>(validationResponseData.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                });

                if (validationResponse == null || validationResponse.Email == null || validationResponse.UserAccountType == null)
                {
                    return null;
                }

                return new ValidationResponse
                {
                    UserId = validationResponse.UserId,
                    UserAccountType = validationResponse.UserAccountType,
                    Email = validationResponse.Email,
                    UserType = validationResponse.UserType,
                    IsValid = validationResponse.IsValid
                };
            }

            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in LoginRepository. Error message: \"{ex.Message}\"");
                return null;
            }
        }

        public async Task<LoginResponse> LoginUserAsync(ValidationResponse validationResponse)
        {
            try
            {
                if (validationResponse == null || !validationResponse.IsValid)
                {
                    return new LoginResponse
                    {
                        Message = "Login failed.",
                        IsLoginSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                var token = _jwtService.GenerateToken(validationResponse);

                var sessionRequest = new SessionRequest
                {
                    UserId = validationResponse.UserId,
                    AccessToken = token,
                    Expires = DateTime.UtcNow.AddMinutes(60),
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(sessionRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClientSession.PostAsync("create", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResponse
                    {
                        Message = "Failed to create session",
                        IsLoginSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                return new LoginResponse
                {
                    UserId = sessionRequest.UserId,
                    UserAccountType = validationResponse.UserAccountType,
                    AccessToken = sessionRequest.AccessToken,
                    Expires = sessionRequest.Expires,
                    Message = "User logged in successfully",
                    IsLoginSuccessful = true,
                    IsConnectedToService = true
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Message = ex.Message,
                    IsLoginSuccessful = false,
                    IsConnectedToService = false
                };
            }
        }

        public async Task<LogoutResponse> LogoutAsync(string token)
        {
            try
            {
                var sessionResponse = await _httpClientSession.PutAsync($"logout/{token}", null);
                if (!sessionResponse.IsSuccessStatusCode)
                {
                    return new LogoutResponse
                    {
                        Message = "Failed to logout",
                        IsLogoutSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                return new LogoutResponse
                {
                    Message = "User logged out successfully",
                    IsLogoutSuccessful = true,
                    IsConnectedToService = true
                };
            }
            catch (Exception ex)
            {
                return new LogoutResponse
                {
                    Message = ex.Message,
                    IsLogoutSuccessful = false,
                    IsConnectedToService = false
                };
            }

        }
    }
}
