using UserService.DTO;
using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersDevAsync();
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> CreateUserAsync(UserCreation userCreation);
        Task<UserDTO?> UpdateUserAsync(int id, UserUpdate userUpdate);
        Task<UserDTO?> DeleteUserAsync(int id);
        Task<ValidationResponseDTO?> ValidateUserAsync(string email, string password);
    }
}
