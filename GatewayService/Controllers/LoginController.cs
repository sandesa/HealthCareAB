using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly HttpClient _loginClient;

        public LoginController(IHttpClientFactory factory)
        {
            _loginClient = factory.CreateClient("LoginService");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );
            try
            {
                var response = await _loginClient.PostAsync(string.Empty, jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new { Message = "Invalid credentials", IsLoginSuccessful = false, IsConnectedToService = true });
                }

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                var token = jsonResponse.GetProperty("accessToken").ToString();
                var email = jsonResponse.GetProperty("email").ToString();

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true in production
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("auth_token", token, cookieOptions);

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsLoginSuccessful = false, IsConnectedToService = false });
            }
        }
    }
}
