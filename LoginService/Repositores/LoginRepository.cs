using LoginService.Interfaces;
using LoginService.Models;
using System.Text;
using System.Text.Json;

namespace LoginService.Repositores
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpClient _httpClientUser;

        public LoginRepository(IHttpClientFactory factory)
        {
            _httpClientUser = factory.CreateClient("UserService");
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
                var token = jsonResponse.GetProperty("data").GetString();

                return new ValidationResponse
                {
                    Token = token,
                    Message = "User validated successfully",
                    IsValid = true,
                    IsConnectedToService = true,
                };
            }

            catch (HttpRequestException ex)
            {
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
                if (!validationResponse.IsValid)
                {
                    return new LoginResponse
                    {
                        Message = validationResponse.Message,
                        IsLoginSuccessful = false,
                        IsConnectedToService = true
                    };
                }

                return new LoginResponse
                {
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
