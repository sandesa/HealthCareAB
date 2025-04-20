using LoginService.Interfaces;
using LoginService.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoginService.Repositores
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpClient _httpClientUser;
        private readonly HttpClient _httpClientSession;

        public LoginRepository(IHttpClientFactory factory)
        {
            _httpClientUser = factory.CreateClient("UserService");
            _httpClientSession = factory.CreateClient("SessionService");
        }

        public async Task<ValidationResponse> ValidateUserAsync(LoginRequest request)
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
                    return new ValidationResponse
                    {
                        Message = "Invalid credentials",
                        IsValid = false,
                        IsConnectedToService = true
                    };
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

                if (validationResponse == null || validationResponse.Email == null || validationResponse.AccessToken == null)
                {
                    return new ValidationResponse
                    {
                        Message = "Invalid credentials",
                        IsValid = false,
                        IsConnectedToService = true
                    };
                }

                return new ValidationResponse
                {
                    Email = validationResponse.Email,
                    AccessToken = validationResponse.AccessToken,
                    Expires = validationResponse.Expires,
                    Message = "User validated successfully",
                    IsValid = true,
                    IsConnectedToService = true,
                };
            }

            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in LoginRepository. Error message: \"{ex.Message}\"");
                return new ValidationResponse
                {
                    IsConnectedToService = false,
                    IsValid = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<LoginResponse> LoginUserAsync(ValidationResponse validationResponse)
        {
            try
            {
                if (validationResponse.IsValid == null)
                {
                    return new LoginResponse
                    {
                        Message = "Invalid credentials",
                        IsLoginSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                if ((bool)!validationResponse.IsValid)
                {
                    return new LoginResponse
                    {
                        Message = validationResponse.Message,
                        IsLoginSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                var sessionRequest = new SessionRequest
                {
                    Email = validationResponse.Email,
                    AccessToken = validationResponse.AccessToken,
                    Expires = validationResponse.Expires
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
                    Email = validationResponse.Email,
                    AccessToken = validationResponse.AccessToken,
                    Expires = validationResponse.Expires,
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
    }
}
