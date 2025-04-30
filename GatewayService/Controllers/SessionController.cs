using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : Controller
    {
        private readonly HttpClient _sessionClient;

        public SessionController(IHttpClientFactory httpClientFactory)
        {
            _sessionClient = httpClientFactory.CreateClient("SessionService");
        }

        [EndpointSummary("GET dev")]
        [EndpointDescription("Get FULL information about all sessions (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetSessionsDevAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "dev");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("GET by ID")]
        [EndpointDescription("Get information about session by ID\n\nRequired roles \"Developer, Admin, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSessionByIdAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"get/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("POST new Session")]
        [EndpointDescription("Create new session\n\nNo required roles\n\nUser must NOT be logged in")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLoginSessionAsync([FromBody] SessionCreate sessionCreate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }
                var jsonContent = new StringContent(JsonSerializer.Serialize(sessionCreate),
                    Encoding.UTF8,
                    "application/json"
                );
                HttpRequestMessage requestMessage = new(HttpMethod.Post, "create")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("POST Logout")]
        [EndpointDescription("Update session (logout)\n\nNo required roles\n\nUser must NOT be logged in but must provide a valid token")]
        [HttpPut("logout")]
        public async Task<IActionResult> LogoutSessionAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"logout/{token}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("PUT Session")]
        [EndpointDescription("Update session (login)\n\nRequired roles \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSessionAsync(int id, [FromBody] SessionUpdate sessionUpdate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(sessionUpdate),
                    Encoding.UTF8,
                    "application/json"
                );
                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update/{id}")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("DELETE Session")]
        [EndpointDescription("Delete session\n\nRequired roles \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSessionAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _sessionClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }
    }
}
