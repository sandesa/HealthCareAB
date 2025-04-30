using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/logout")]
    public class LogoutController : Controller
    {
        private readonly HttpClient _logoutClient;

        public LogoutController(IHttpClientFactory factory)
        {
            _logoutClient = factory.CreateClient("LoginService");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Post, $"logout/{token}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _logoutClient.SendAsync(requestMessage);

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                if (response.IsSuccessStatusCode)
                {
                    Response.Cookies.Delete("auth_token");
                    Response.Cookies.Delete("user_type");
                    Response.Cookies.Delete("logged_in");
                }

                return StatusCode((int)response.StatusCode, jsonResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
