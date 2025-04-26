using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly HttpClient _userClient;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _userClient = httpClientFactory.CreateClient("UserService");
        }

        [HttpGet("dev")]
        public async Task<IActionResult> GetUsersDevAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "dev");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _userClient.SendAsync(requestMessage);

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "get-all");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _userClient.SendAsync(requestMessage);

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id, [FromQuery] string? token)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var cookieToken) || string.IsNullOrWhiteSpace(cookieToken))
                {
                    if(token == null)
                    {
                        return Unauthorized(new { Message = "Missing or invalid token." });
                    }
                }

                var authToken = token ?? cookieToken;

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"get/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token ?? cookieToken);

                var response = await _userClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreation userCreation)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(userCreation),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _userClient.PostAsync("create", jsonContent);

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdate userUpdate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(userUpdate),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update/{id}")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _userClient.SendAsync(requestMessage);

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _userClient.SendAsync(requestMessage);

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
