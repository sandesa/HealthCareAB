using UserService.DTO;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO<IEnumerable<User>>> GetUsersDevAsync()
        {
            try
            {
                var users = await _userRepository.GetUsersDevAsync();
                if (!users.Any())
                {
                    return new ResponseDTO<IEnumerable<User>>
                    {
                        Message = "No users found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<User>>
                {
                    Data = users,
                    Message = "Users retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting DEV User data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<IEnumerable<User>>
                {
                    Message = "An error occurred when getting DEV User data.",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<UserDTO>>> GetUsersAsync()
        {
            try
            {
                var userDtos = await _userRepository.GetUsersAsync();

                if (!userDtos.Any())
                {
                    return new ResponseDTO<IEnumerable<UserDTO>>
                    {
                        Message = "No users found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<UserDTO>>
                {
                    Data = userDtos,
                    Message = "Users retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting User data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<IEnumerable<UserDTO>>
                {
                    Message = "An error occurred when getting User data.",
                };
            }
        }

        public async Task<ResponseDTO<UserDTO>> CreateUserAsync(UserCreation userCreation)
        {
            try
            {
                var userDto = await _userRepository.CreateUserAsync(userCreation);

                if (userDto == null)
                {
                    return new ResponseDTO<UserDTO>
                    {
                        Message = "User creation failed.",
                    };
                }

                return new ResponseDTO<UserDTO>
                {
                    Data = userDto,
                    Message = "User created successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when creating User data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<UserDTO>
                {
                    Message = "An error occurred when creating User data.",
                };
            }
        }

        public async Task<ResponseDTO<UserDTO>> UpdateUserAsync(int id, UserUpdate userUpdate)
        {
            try
            {
                var userDto = await _userRepository.UpdateUserAsync(id, userUpdate);
                if (userDto == null)
                {
                    return new ResponseDTO<UserDTO>
                    {
                        Message = "User not found.",
                    };
                }
                return new ResponseDTO<UserDTO>
                {
                    Data = userDto,
                    Message = "User updated successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating User data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<UserDTO>
                {
                    Message = "An error occurred when updating User data.",
                };
            }
        }

        public async Task<ResponseDTO<UserDTO>> DeleteUserAsync(int id)
        {
            try
            {
                var userDto = await _userRepository.DeleteUserAsync(id);
                if (userDto == null)
                {
                    return new ResponseDTO<UserDTO>
                    {
                        Message = "User not found.",
                    };
                }
                return new ResponseDTO<UserDTO>
                {
                    Data = userDto,
                    Message = "User deleted successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when deleting User data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<UserDTO>
                {
                    Message = "An error occurred when deleting User data.",
                };
            }
        }


    }
}
