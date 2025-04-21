using AvailabilityService;
using AvailabilityService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class AvailabilityServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public AvailabilityServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var token = JwtTokenGeneratorTest.GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetAvailabilitiesDev_ReturnsOk_WhenIsSuccess()
        {
            var response = await _client.GetAsync("/api/availability/dev");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var availabilities = jsonResponse.GetProperty("data");

            Assert.NotEmpty(availabilities.EnumerateArray());
            Assert.Equal("Availabilities DEV retrieved successfully.", jsonResponse.GetProperty("message").ToString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task GetAvailabilitiesByCaregiverId_ReturnsOk_WhenIsSuccess()
        {
            int caregiverId = 1;

            var response = await _client.GetAsync($"/api/availability/caregiver/{caregiverId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var availabilities = jsonResponse.GetProperty("data");

            Assert.NotEmpty(availabilities.EnumerateArray());
            Assert.Equal("Availabilities retrieved successfully.", jsonResponse.GetProperty("message").ToString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task GetAvailabilitiesByDate_ReturnsOk_WhenIsSuccess()
        {
            string date = "2025-10-01";

            var response = await _client.GetAsync($"/api/availability/date/{date}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var availabilities = jsonResponse.GetProperty("data");

            Assert.NotEmpty(availabilities.EnumerateArray());
            Assert.Equal("Availabilities retrieved successfully.", jsonResponse.GetProperty("message").ToString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task GetAvailabilityById_ReturnsOk_WhenIsSucces()
        {
            int id = 1;

            var response = await _client.GetAsync($"/api/availability/id/{id}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
            Assert.Equal("Availability retrieved successfully.", jsonResponse.GetProperty("message").ToString());
        }

        [Fact]
        public async Task CreateAvailability_ReturnsOk_WhenIsSuccess()
        {
            AvailabilityCreate newAvailability = new()
            {
                CaregiverId = 9,
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(5).AddHours(8),
                Notes = "Test",
            };

            var response = await _client.PostAsJsonAsync("/api/availability/create", newAvailability);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdAvailability = jsonResponse.GetProperty("data");

            Assert.Equal(9, createdAvailability.GetProperty("caregiverId").GetInt32());
            Assert.Equal(DateTime.Now.AddDays(5).ToShortDateString(), createdAvailability.GetProperty("startTime").GetDateTime().ToShortDateString());
            Assert.Equal(DateTime.Now.AddDays(5).AddHours(8).ToShortDateString(), createdAvailability.GetProperty("endTime").GetDateTime().ToShortDateString());
            Assert.Equal("Test", createdAvailability.GetProperty("notes").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
            Assert.Equal("Availability created successfully.", jsonResponse.GetProperty("message").ToString());
        }

        [Fact]
        public async Task UpdateAvailability_ReturnsOk_WhenIsSuccess()
        {
            int id = 1;
            AvailabilityUpdate availabilityUpdate = new()
            {
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(5).AddHours(8),
                Notes = "Updated Test",
            };

            var response = await _client.PutAsJsonAsync($"/api/availability/update/{id}", availabilityUpdate);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var updatedAvailability = jsonResponse.GetProperty("data");

            Assert.Equal(DateTime.Now.AddDays(5).ToShortDateString(), updatedAvailability.GetProperty("startTime").GetDateTime().ToShortDateString());
            Assert.Equal(DateTime.Now.AddDays(5).AddHours(8).ToShortDateString(), updatedAvailability.GetProperty("endTime").GetDateTime().ToShortDateString());
            Assert.Equal("Updated Test", updatedAvailability.GetProperty("notes").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
            Assert.Equal("Availability updated successfully.", jsonResponse.GetProperty("message").ToString());
        }

        [Fact]
        public async Task DeleteAvailability_ReturnsOk_WhenIsSuccess()
        {
            int id = 5;

            var response = await _client.DeleteAsync($"/api/availability/delete/{id}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

            Assert.Equal("Availability deleted successfully.", jsonResponse.GetProperty("message").ToString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }
    }
}
