using LoginService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginService.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ValidationResponse response)
        {
            if (response.UserAccountType == null || response.Email == null || response.UserType == null || !response.IsValid)
            {
                throw new ArgumentNullException(nameof(response), "Response cannot be null.");
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
                new(ClaimTypes.NameIdentifier, response.UserId.ToString()),
                new(ClaimTypes.Role, response.UserAccountType),
                new(ClaimTypes.Email, response.Email),
                new("user_type", response.UserType)
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

            return token;
        }
    }
}
