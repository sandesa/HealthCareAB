using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.DTO;
using UserService.Interfaces;
using UserService.Models;
using UserService.Services;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly IHashingRepository _hashing;
        private readonly IValidationRepository _verify;
        private readonly UserMappingService _mapper;
        private readonly JwtTokenService _token;

        public UserRepository(UserDbContext context, UserMappingService mapper, IHashingRepository hashing, IValidationRepository verify, JwtTokenService token)
        {
            _context = context;
            _hashing = hashing;
            _verify = verify;
            _mapper = mapper;
            _token = token;
        }

        public async Task<IEnumerable<User>> GetUsersDevAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(user => _mapper.UserToDto(user));
            return userDtos;
        }

        public async Task<UserDTO?> CreateUserAsync(UserCreation userCreation)
        {
            if (userCreation.PasswordHash == null)
            {
                return null;
            }

            userCreation.PasswordHash = _hashing.HashPassword(userCreation.PasswordHash);
            var user = _mapper.CreationToUser(userCreation);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var userDTO = _mapper.UserToDto(user);
            return userDTO;
        }

        public async Task<UserDTO?> UpdateUserAsync(int id, UserUpdate userUpdate)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }
            if (userUpdate.PasswordHash != null)
            {
                userUpdate.PasswordHash = _hashing.HashPassword(userUpdate.PasswordHash);
            }
            existingUser = _mapper.UpdateToUser(existingUser, userUpdate);
            existingUser.UpdatedAt = DateTime.Now;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            var userDTO = _mapper.UserToDto(existingUser);
            return userDTO;
        }

        public async Task<UserDTO?> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            var userDTO = _mapper.UserToDto(user);
            return userDTO;
        }

        public async Task<ValidationResponse?> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine($"Could not find user with username \"{email}\"");
                return null;
            }

            bool isValid = user.PasswordHash != null && _verify.VerifyPassword(password, user.PasswordHash);

            if (!isValid)
            {
                Console.WriteLine($"Failed to validate user with username \"{email}\" and password.");
                return null;
            }

            var validationResponse = _token.GenerateToken(user);

            Console.WriteLine($"User with username \"{email}\" and password validated.");
            return new ValidationResponse
            {
                Email = user.Email,
                AccessToken = validationResponse.AccessToken,
                Expires = validationResponse.Expires,
            };
        }

    }
}
