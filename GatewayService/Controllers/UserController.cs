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

        [EndpointSummary("GET dev")]
        [EndpointDescription("Get FULL information about all users (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("GET all")]
        [EndpointDescription("Get information about all users\n\nRequired roles \"Developer, Admin, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("GET user by ID")]
        [EndpointDescription("Get information about user by ID\n\nNo role required\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("get")]
        public async Task<IActionResult> GetUserByIdAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "get");
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

        [EndpointSummary("POST new User")]
        [EndpointDescription("Create new user\n\nNo role required\n\nUser must NOT be logged in")]
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

        [EndpointSummary("PUT User")]
        [EndpointDescription("Update user data\n\nNo role required\n\nUser must be logged in (have a valid active token)")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdate userUpdate)
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

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update")
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

        [EndpointSummary("DELETE User")]
        [EndpointDescription("Delete user\n\nNo role required\n\nUser must be logged in (have a valid active token)")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete");
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
