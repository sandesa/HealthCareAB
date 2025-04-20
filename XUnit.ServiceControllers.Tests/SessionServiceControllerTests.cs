using Microsoft.AspNetCore.Mvc.Testing;
using SessionService;
using SessionService.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class SessionServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public SessionServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetSessionsDev_ReturnsOk_WhenIsSuccess()
        {
            //int numberOfSessions = 10;

            var response = await _client.GetAsync("/api/session/dev");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var sessions = jsonResponse.GetProperty("data");
        }

        [Fact]
        public async Task GetSessionById_ReturnsOk_WhenIsSuccess()
        {
            int sessionId = 1;
            var response = await _client.GetAsync($"/api/session/get/{sessionId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
        }

        [Fact]
        public async Task CreateSession_ReturnsOk_WhenCreated()
        {
            SessionCreate sessionCreate = new()
            {
                Email = "testEmail",
                AccessToken = "testToken",
                Expires = DateTime.Now.AddMinutes(60),
                Login = DateTime.Now
            };

            var response = await _client.PostAsJsonAsync("/api/session/create", sessionCreate);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionCreate.Email, session.GetProperty("email").GetString());
            Assert.Equal(sessionCreate.AccessToken, session.GetProperty("accessToken").GetString());
        }

        [Fact]
        public async Task UpdateSessionLogout_ReturnsOk_WhenUpdated()
        {
            int sessionId = 1;

            var response = await _client.PutAsJsonAsync($"/api/session/logout/{sessionId}", sessionId);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
            Assert.Equal(DateTime.Now.ToShortDateString(), session.GetProperty("logout").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task UpdateSession_ReturnsOk_WhenUpdated()
        {
            int sessionId = 1;
            SessionUpdate sessionUpdate = new()
            {
                Email = "NewTestEmail",
                AccessToken = "NewTestToken"
            };

            var response = await _client.PutAsJsonAsync($"/api/session/update/{sessionId}", sessionUpdate);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
            Assert.Equal(sessionUpdate.Email, session.GetProperty("email").GetString());
            Assert.Equal(sessionUpdate.AccessToken, session.GetProperty("accessToken").GetString());
        }

        [Fact]
        public async Task DeleteSession_ReturnsOk_WhenDeleted()
        {
            int sessionId = 1;
            var response = await _client.DeleteAsync($"/api/session/delete/{sessionId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
        }
    }
}
