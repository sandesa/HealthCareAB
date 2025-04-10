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

        public string GenerateToken(User user)
        {
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
                new (ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            claims.AddRange(user.UserAccountType.Select(type => new Claim(ClaimTypes.Role, type)));
            claims.AddRange(user.UserType.Select(type => new Claim(ClaimTypes.Role, type)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenExpiration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
