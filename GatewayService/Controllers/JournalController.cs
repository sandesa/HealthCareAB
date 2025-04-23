using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/journal")]
    public class JournalController : Controller
    {
        private readonly HttpClient _journalClient;

        public JournalController(IHttpClientFactory httpClientFactory)
        {
            _journalClient = httpClientFactory.CreateClient("JournalService");
        }

        [HttpGet("dev")]
        public IActionResult GetJournalsDevAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "dev");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = _journalClient.SendAsync(requestMessage);
                var jsonResponse = response.Result.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.Result.StatusCode, jsonResponse.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("user/{patientId}")]
        public async Task<IActionResult> GetJournalsByUserIdAsync(int patientId)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }
                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"user/{patientId}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("caregiver/{caregiverId}")]
        public async Task<IActionResult> GetJournalsByCaregiverIdAsync(int caregiverId)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }
                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"caregiver/{caregiverId}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetJournalByIdAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalAsync([FromBody] JournalCreation journalCreation)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(journalCreation),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Post, "create")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJournalAsync(int id, [FromBody] JournalUpdate journalUpdate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(journalUpdate),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update/{id}")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJournalAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _journalClient.SendAsync(requestMessage);
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
