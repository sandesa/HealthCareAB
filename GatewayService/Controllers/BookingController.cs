using GatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly HttpClient _bookingClient;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _bookingClient = httpClientFactory.CreateClient("BookingService");
        }

        [EndpointSummary("GET dev")]
        [EndpointDescription("Get FULL information about all bookings (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetBookingsDevAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, "dev");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("GET by caregiver ID")]
        [EndpointDescription("Get information about all bookings with caregiver ID\n\nRequired roles: \"Developer, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("caregiver")]
        public async Task<IActionResult> GetBookingsByCaregiverIdAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"caregiver");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("GET by user ID")]
        [EndpointDescription("Get all bookings for patient with ID\n\nRequired roles: \"User, Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("user")]
        public async Task<IActionResult> GetBookingsByPatientIdAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"user");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("GET by ID")]
        [EndpointDescription("Get information about booking by ID\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }
                HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("POST new Booking")]
        [EndpointDescription("Create new booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingCreation bookingCreation)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(bookingCreation),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Post, "create")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("PUT Booking")]
        [EndpointDescription("Update booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] BookingUpdate bookingUpdate)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var jsonContent = new StringContent(JsonSerializer.Serialize(bookingUpdate),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"update/{id}")
                {
                    Content = jsonContent
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("PUT cancel Booking")]
        [EndpointDescription("Cancel booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBookingAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Put, $"cancel/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                return StatusCode((int)response.StatusCode, jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }

        [EndpointSummary("DELETE Booking")]
        [EndpointDescription("Delete booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("auth_token", out var token) || string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                HttpRequestMessage requestMessage = new(HttpMethod.Delete, $"delete/{id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _bookingClient.SendAsync(requestMessage);
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
