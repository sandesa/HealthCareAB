using FeedbackService;
using FeedbackService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class FeedbackServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public FeedbackServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var token = JwtTokenGeneratorTest.GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetFeedbacksDev_ReturnsOk_WhenIsSuccess()
        {
            var response = await _client.GetAsync("/api/feedback/dev");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var feedbacks = jsonResponse.GetProperty("data");

            Assert.NotEmpty(feedbacks.EnumerateArray());
        }

        [Fact]
        public async Task GetFeedbackById_ReturnsOk_WhenIsSuccess()
        {
            int feedbackId = 1;

            var response = await _client.GetAsync($"/api/feedback/{feedbackId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var message = jsonResponse.GetProperty("message").GetString();
            var isSuccess = jsonResponse.GetProperty("isSuccess").GetBoolean();

            Assert.Equal("Feedback successfully fetched.", message);
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task GetFeedbackByBookingId_ReturnsOk_WhenIsSuccess()
        {
            int bookingId = 1;

            var response = await _client.GetAsync($"/api/feedback/booking/{bookingId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var feedback = jsonResponse.GetProperty("data");

            Assert.Equal(1, feedback.GetProperty("bookingId").GetInt32());
            Assert.Equal("Feedback successfully fetched.", jsonResponse.GetProperty("message").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task CreateFeedback_ReturnsOk_WhenFeedbackCreated()
        {
            FeedbackCreate newFeedback = new()
            {
                BookingId = 1,
                Rating = 5,
                Comment = "Test"
            };
            var response = await _client.PostAsJsonAsync("/api/feedback/create", newFeedback);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var feedback = jsonResponse.GetProperty("data");

            Assert.Equal(1, feedback.GetProperty("bookingId").GetInt32());
            Assert.Equal(5, feedback.GetProperty("rating").GetInt32());
            Assert.Equal("Test", feedback.GetProperty("comment").GetString());
            Assert.Equal(DateTime.Now.ToShortDateString(), feedback.GetProperty("created").GetDateTime().ToShortDateString());
            Assert.Equal("Feedback successfully created.", jsonResponse.GetProperty("message").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task UpdateFeedback_ReturnsOk_WhenFeedbackUpdated()
        {
            int feedbackId = 1;
            FeedbackUpdate updatedFeedback = new()
            {
                Rating = 5,
                Comment = "Updated Test"
            };

            var response = await _client.PutAsJsonAsync($"/api/feedback/update/{feedbackId}", updatedFeedback);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var feedback = jsonResponse.GetProperty("data");

            Assert.Equal(5, feedback.GetProperty("rating").GetInt32());
            Assert.Equal("Updated Test", feedback.GetProperty("comment").GetString());
            Assert.Equal(DateTime.Now.ToShortDateString(), feedback.GetProperty("updated").GetDateTime().ToShortDateString());
            Assert.Equal("Feedback successfully updated.", jsonResponse.GetProperty("message").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }

        [Fact]
        public async Task DeleteFeedback_ReturnsOk_WhenFeedbackDeleted()
        {
            int feedbackId = 1;

            var response = await _client.DeleteAsync($"/api/feedback/delete/{feedbackId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

            Assert.Equal("Feedback successfully deleted.", jsonResponse.GetProperty("message").GetString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }
    }
}