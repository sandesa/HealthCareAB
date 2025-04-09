using UserService.DTO;
using UserService.Models;

namespace UserService.Services
{
    public class UserMappingService
    {
        public UserDTO UserToDto(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserType = user.UserType,
                UserAccountType = user.UserAccountType,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public User DtoToUser(UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DateOfBirth = userDTO.DateOfBirth,
                UserType = userDTO.UserType,
                UserAccountType = userDTO.UserAccountType,
                CreatedAt = userDTO.CreatedAt,
                UpdatedAt = userDTO.UpdatedAt
            };
        }


        public User UserToUser(User existingUser, UserUpdate userUpdate)
        {
            existingUser.Id = existingUser.Id;
            existingUser.Email = userUpdate.Email ?? existingUser.Email;
            existingUser.PasswordHash = userUpdate.PasswordHash ?? existingUser.PasswordHash;
            existingUser.PhoneNumber = userUpdate.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.FirstName = userUpdate.FirstName ?? existingUser.FirstName;
            existingUser.LastName = userUpdate.LastName ?? existingUser.LastName;
            existingUser.DateOfBirth = userUpdate.DateOfBirth ?? existingUser.DateOfBirth;
            existingUser.UserType = userUpdate.UserType ?? existingUser.UserType;
            existingUser.UserAccountType = userUpdate.UserAccountType ?? existingUser.UserAccountType;
            existingUser.CreatedAt = existingUser.CreatedAt;
            return existingUser;
        }

        public User ModificationToUser(UserCreation userCreation)
        {
            return new User
            {
                Email = userCreation.Email,
                PasswordHash = userCreation.PasswordHash,
                PhoneNumber = userCreation.PhoneNumber,
                FirstName = userCreation.FirstName,
                LastName = userCreation.LastName,
                DateOfBirth = userCreation.DateOfBirth,
            };
        }
    }
}
