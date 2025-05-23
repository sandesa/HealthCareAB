﻿using Microsoft.AspNetCore.Mvc.Testing;
using SessionService;
using SessionService.Models;
using System.Net.Http.Headers;
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

            var token = JwtTokenGeneratorTest.GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                UserId = 5,
                AccessToken = "testToken",
                Expires = DateTime.UtcNow.AddMinutes(60),
            };

            var response = await _client.PostAsJsonAsync("/api/session/create", sessionCreate);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionCreate.UserId, session.GetProperty("userId").GetInt16());
            Assert.Equal(sessionCreate.AccessToken, session.GetProperty("accessToken").GetString());
        }

        //[Fact]
        //public async Task UpdateSessionLogout_ReturnsOk_WhenUpdated()
        //{
        //    int sessionId = 1;

        //    var response = await _client.PutAsJsonAsync($"/api/session/logout/{sessionId}", sessionId);
        //    response.EnsureSuccessStatusCode();
        //    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
        //    var session = jsonResponse.GetProperty("data");
        //    Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
        //    Assert.Equal(DateTime.UtcNow.ToShortDateString(), session.GetProperty("logout").GetDateTime().ToShortDateString());
        //}

        [Fact]
        public async Task UpdateSession_ReturnsOk_WhenUpdated()
        {
            int sessionId = 1;
            SessionUpdate sessionUpdate = new()
            {
                UserId = 3,
                AccessToken = "NewTestToken"
            };

            var response = await _client.PutAsJsonAsync($"/api/session/update/{sessionId}", sessionUpdate);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var session = jsonResponse.GetProperty("data");
            Assert.Equal(sessionId, session.GetProperty("id").GetInt32());
            Assert.Equal(sessionUpdate.UserId, session.GetProperty("userId").GetInt16());
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
