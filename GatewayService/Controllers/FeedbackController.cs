using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : Controller
    {
        private readonly HttpClient _feedbackClient;

        public FeedbackController(IHttpClientFactory httpClientFactory)
        {
            _feedbackClient = httpClientFactory.CreateClient("FeedbackService");
        }

        [HttpGet("feedback/dev")]
        public async Task<IActionResult> GetFeedbacksDevAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "dev");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _feedbackClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("feedback/{id}")]
        public async Task<IActionResult> GetFeedbackByIdAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _feedbackClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpGet("feedback/booking/{bookingId}")]
        public IActionResult GetFeedbackAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"booking/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = _feedbackClient.SendAsync(requestMessage);
                var jsonResponse = response.Result.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.Result.StatusCode, jsonResponse.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPost("feedback/create")]
        public async Task<IActionResult> CreateFeedbackAsync([FromBody] FeedbackCreation feedbackCreation)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(feedbackCreation),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Post, "create")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _feedbackClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpPut("feedback/update/{id}")]
        public async Task<IActionResult> UpdateFeedbackAsync(int id, [FromBody] FeedbackUpdate feedbackUpdate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(feedbackUpdate),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update/{id}")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _feedbackClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [HttpDelete("feedback/delete/{id}")]
        public async Task<IActionResult> DeleteFeedbackAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _feedbackClient.SendAsync(requestMessage);
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
