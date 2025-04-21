using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XUnit.ServiceControllers.Tests
{
    internal class JwtTokenGeneratorTest
    {
        public static string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f9f5657ab12ab16b0cad95e4c6fc8c73b0789045ff1fadc32a9c58cf74b0846978af8925f22b8e98a30ecba09315f04d1c220d6d522650633c588935c1f6b88c"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Role, "Developer"),
                new(ClaimTypes.Email, "Test.Email@gmail.com"),
                new("user_type", "TestType")
            };

            var token = new JwtSecurityToken(
                issuer: "AppIssuer",
                audience: "AppUser",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
