using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using UserService;
using UserService.Interfaces;
using UserService.Models;
using UserService.Utilities.StdDef;

namespace XUnit.ServiceControllers.Tests
{
    public class UserServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IValidationRepository _verify;

        public UserServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var mockVerify = new Mock<IValidationRepository>();
            mockVerify.Setup(v => v.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _verify = mockVerify.Object;

            var token = JwtTokenGeneratorTest.GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetUsersDev_ReturnsOk_WhenIsSuccess()
        {
            int numberOfUsers = 10;

            var response = await _client.GetAsync("/api/user/dev");

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var users = jsonResponse.GetProperty("data");

            Assert.Equal(numberOfUsers, users.GetArrayLength());
        }

        [Fact]
        public async Task GetUsers_ReturnsOk_WhenIsSuccess()
        {
            int numberOfUsers = 10;

            var response = await _client.GetAsync("/api/user/get-all");

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var users = jsonResponse.GetProperty("data");

            Assert.Equal(numberOfUsers, users.GetArrayLength());
        }

        [Fact]
        public async Task CreateUser_ReturnsOk_WhenUserCreated()
        {
            UserCreation newUser = new()
            {
                Email = "test",
                PasswordHash = "123",
                PhoneNumber = "123456",
                FirstName = "Test",
                LastName = "Testsson",
                DateOfBirth = new DateTime(1998, 03, 02)
            };

            var response = await _client.PostAsJsonAsync("/api/user/create", newUser);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdUser = jsonResponse.GetProperty("data");

            Assert.Equal("test", createdUser.GetProperty("email").GetString());
            //Assert.True(_verify.VerifyPassword(newUser.PasswordHash, createdUser.GetProperty("passwordHash").GetString()));
            Assert.Equal("123456", createdUser.GetProperty("phoneNumber").GetString());
            Assert.Equal("Test", createdUser.GetProperty("firstName").GetString());
            Assert.Equal("Testsson", createdUser.GetProperty("lastName").GetString());
            Assert.Equal("1998-03-02T00:00:00", createdUser.GetProperty("dateOfBirth").GetString());
        }

        [Fact]
        public async Task UpdateUser_ReturnsOk_WhenUserUpdated()
        {
            int id = 1;
            UserUpdate updatedUser = new()
            {
                Email = "test",
                PasswordHash = "123",
                PhoneNumber = "123456",
                FirstName = "Test",
                LastName = "Testsson",
                DateOfBirth = new DateTime(1998, 03, 02),
                UserType = UserType.Doctor.ToString(),
                UserAccountType = UserAccountType.Developer.ToString()
            };

            var response = await _client.PutAsJsonAsync($"/api/user/update", updatedUser);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdUser = jsonResponse.GetProperty("data");

            Assert.Equal(id, createdUser.GetProperty("id").GetInt32());
            Assert.Equal("test", createdUser.GetProperty("email").GetString());
            //Assert.True(_verify.VerifyPassword(newUser.PasswordHash, createdUser.GetProperty("passwordHash").GetString()));
            Assert.Equal("123456", createdUser.GetProperty("phoneNumber").GetString());
            Assert.Equal("Test", createdUser.GetProperty("firstName").GetString());
            Assert.Equal("Testsson", createdUser.GetProperty("lastName").GetString());
            Assert.Equal("1998-03-02T00:00:00", createdUser.GetProperty("dateOfBirth").GetString());
            Assert.Equal("Doctor", createdUser.GetProperty("userType").GetString());
            Assert.Equal("Developer", createdUser.GetProperty("userAccountType").GetString());
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk_WhenUserDeleted()
        {
            int id = 1;

            var response = await _client.DeleteAsync($"/api/user/delete");

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var user = jsonResponse.GetProperty("data");
            var userId = user.GetProperty("id").GetInt32();
            var isSuccess = jsonResponse.GetProperty("isSuccess").GetBoolean();

            Assert.Equal(id, userId);
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task ValidateUser_ReturnsOk_WhenCorrectEmailAndPassword()
        {
            ValidationRequest validationRequest = new()
            {
                Email = "test1.testsson@gmail.com",
                Password = "123"
            };

            var response = await _client.PostAsJsonAsync($"/api/user/validate", validationRequest);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var validationResponse = jsonResponse.GetProperty("data");
            var email = validationResponse.GetProperty("email").GetString();

            Assert.NotNull(validationResponse.GetProperty("userAccountType").GetString());
            Assert.Equal(validationRequest.Email, email);
            Assert.NotNull(validationResponse.GetProperty("userType").GetString());
            Assert.True(validationResponse.GetProperty("isValid").GetBoolean());
        }
    }

}
