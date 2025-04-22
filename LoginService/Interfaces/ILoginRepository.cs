using LoginService.Models;

namespace LoginService.Interfaces
{
    public interface ILoginRepository
    {
        Task<ValidationResponse?> ValidateUserAsync(LoginRequest request);
        Task<LoginResponse> LoginUserAsync(ValidationResponse validationResponse);
    }
}
