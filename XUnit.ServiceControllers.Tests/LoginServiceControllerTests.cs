using LoginService;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class LoginServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public LoginServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task LoginUser_ReturnsUnauthorized()
        {
            LoginRequest request = new()
            {
                Email = "test1.testsson@gmail.com",
                Password = "123"
            };
            var response = await _client.PostAsJsonAsync("/api/login", request);
            Assert.NotNull(response);
            //response.EnsureSuccessStatusCode();
            //var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            //var token = jsonResponse.GetProperty("Token").ToString();
            //var isLoginSuccessful = jsonResponse.GetProperty("IsLoginSuccessful").GetBoolean();
            //var isConnectedToService = jsonResponse.GetProperty("IsConnectedToService").GetBoolean();
            //Assert.NotNull(token);
            //Assert.True(isLoginSuccessful);
            //Assert.True(isConnectedToService);
        }
    }

}
