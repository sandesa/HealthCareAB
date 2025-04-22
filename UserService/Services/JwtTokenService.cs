


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;

namespace UserService.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ValidationResponse GenerateToken(User user)
        {
            if (user.Email == null || user.UserAccountType == null || user.UserType == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var jwtConfig = _configuration.GetSection("JwtConfig");

            if (jwtConfig == null)
            {
                throw new ArgumentNullException(nameof(jwtConfig), "JWT configuration section is missing.");
            }

            var _secret = jwtConfig.GetRequiredSection("Secret").Value;
            var _issuer = jwtConfig.GetRequiredSection("Issuer").Value;
            var _audience = jwtConfig.GetRequiredSection("Audience").Value;
            var tokenExpiration = jwtConfig.GetRequiredSection("TokenExpiration").Value;

            if (_secret == null || _issuer == null || _audience == null || tokenExpiration == null)
            {
                throw new ArgumentNullException("JWT configuration is missing in the appsettings.");
            }

            var _tokenExpiration = int.Parse(tokenExpiration);

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.UserAccountType),
                    new(ClaimTypes.Email, user.Email),
                    new("user_type", user.UserType)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenExpiration),
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            ValidationResponse response = new()
            {
                Email = user.Email,
                AccessToken = token,
                Expires = tokenDescriptor.ValidTo
            };

            return response;
        }
    }
}
