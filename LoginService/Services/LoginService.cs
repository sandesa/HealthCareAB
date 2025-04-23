using LoginService.Interfaces;
using LoginService.Models;

namespace LoginService.Services
{
    public class LoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<ValidationResponse?> ValidateUserAsync(LoginRequest request)
        {
            var validationResponse = await _loginRepository.ValidateUserAsync(request);

            return validationResponse;
        }

        public async Task<LoginResponse> LoginUserAsync(ValidationResponse validationResponse)
        {
            var loginResponse = await _loginRepository.LoginUserAsync(validationResponse);
            if (!loginResponse.IsConnectedToService)
            {
                return new LoginResponse
                {
                    Message = "Unable to connect to User Service",
                    IsLoginSuccessful = false,
                    IsConnectedToService = false
                };
            }

            if (!loginResponse.IsLoginSuccessful)
            {
                return new LoginResponse
                {
                    Message = "Invalid credentials",
                    IsLoginSuccessful = false,
                    IsConnectedToService = true
                };
            }
            return loginResponse;
        }

        public async Task<LogoutResponse> LogoutAsync(string token)
        {
            var response = await _loginRepository.LogoutAsync(token);

            return response;
        }
    }
}
