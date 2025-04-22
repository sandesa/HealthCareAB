using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/gateway")]
    public class GatewayController : Controller
    {
        private readonly HttpClient _loginClient;
        private readonly HttpClient _userClient;
        private readonly HttpClient _sessionClient;

        public GatewayController(IHttpClientFactory factory)
        {
            _loginClient = factory.CreateClient("LoginService");
            _userClient = factory.CreateClient("UserService");
            _sessionClient = factory.CreateClient("SessionService");
        }

        [HttpPost("login")]
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

        [HttpGet("user/dev")]
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

        [HttpGet("user/get-users")]
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

        [HttpPost("user/create")]
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

        [HttpPut("user/update{id}")]
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

        [HttpDelete("user/delete/{id}")]
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

        [HttpGet("session/dev")]
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

        [HttpGet("session/get/{id}")]
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

        [HttpPost("session/create")]
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

        [HttpPut("session/logout{id}")]
        public async Task<IActionResult> LogoutSessionAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"logout/{id}");
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

        [HttpPut("session/update{id}")]
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

        [HttpDelete("session/delete/{id}")]
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
