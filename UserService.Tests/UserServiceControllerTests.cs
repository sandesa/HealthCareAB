using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UserService.Interfaces;
using UserService.Models;
using UserService.Utilities.StdDef;

namespace UserService.Tests
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
        }

        [Fact]
        public async Task GetUsersDev_ReturnsOk_WhenIsSuccess()
        {
            int numberOfUsers = 10;

            var response = await _client.GetAsync("/api/user/get-all");

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
            var newUser = new UserCreation
            {
                Email = "test",
                PasswordHash = "123",
                PhoneNumber = "123456",
                FirstName = "Test",
                LastName = "Testsson",
                DateOfBirth = new DateOnly(1998, 03, 02)
            };

            var content = new StringContent(
                JsonSerializer.Serialize(newUser),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/user/create", content);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdUser = jsonResponse.GetProperty("data");

            Assert.Equal(newUser.Email, createdUser.GetProperty("email").GetString());
            //Assert.True(_verify.VerifyPassword(newUser.PasswordHash, createdUser.GetProperty("passwordHash").GetString()));
            Assert.Equal(newUser.PhoneNumber, createdUser.GetProperty("phoneNumber").GetString());
            Assert.Equal(newUser.FirstName, createdUser.GetProperty("firstName").GetString());
            Assert.Equal(newUser.LastName, createdUser.GetProperty("lastName").GetString());
            //Assert.Equal(newUser.DateOfBirth.ToString(), createdUser.GetProperty("dateOfBirth").GetString());
        }

        [Fact]
        public async Task UpdateUser_ReturnsOk_WhenUserUpdated()
        {
            int id = 1;
            var updatedUser = new UserUpdate
            {
                Email = "test",
                PasswordHash = "123",
                PhoneNumber = "123456",
                FirstName = "Test",
                LastName = "Testsson",
                DateOfBirth = new DateOnly(1998, 03, 02),
                UserType = [UserType.Doctor.ToString()],
                UserAccountType = [UserAccountType.Developer.ToString()]
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updatedUser),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync($"/api/user/update/{id}", content);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdUser = jsonResponse.GetProperty("data");

            Assert.Equal(id, createdUser.GetProperty("id").GetInt32());
            Assert.Equal(updatedUser.Email, createdUser.GetProperty("email").GetString());
            //Assert.True(_verify.VerifyPassword(newUser.PasswordHash, createdUser.GetProperty("passwordHash").GetString()));
            Assert.Equal(updatedUser.PhoneNumber, createdUser.GetProperty("phoneNumber").GetString());
            Assert.Equal(updatedUser.FirstName, createdUser.GetProperty("firstName").GetString());
            Assert.Equal(updatedUser.LastName, createdUser.GetProperty("lastName").GetString());
            //Assert.Equal(newUser.DateOfBirth.ToString(), createdUser.GetProperty("dateOfBirth").GetString());
            Assert.Equal(updatedUser.UserType[0], createdUser.GetProperty("userType").EnumerateArray().First().GetString());
            Assert.Equal(updatedUser.UserAccountType[0], createdUser.GetProperty("userAccountType").EnumerateArray().First().GetString());
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk_WhenUserDeleted()
        {
            int id = 1;

            var response = await _client.DeleteAsync($"/api/user/delete/{id}");

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
            int id = 1;

            ValidationRequest validationRequest = new ValidationRequest
            {
                Email = "test1.testsson@gmail.com",
                Password = "123"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(validationRequest),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync($"/api/user/validate", content);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var userId = jsonResponse.GetProperty("data").GetInt32();
            var isSuccess = jsonResponse.GetProperty("isSuccess").GetBoolean();

            Assert.True(isSuccess);
            Assert.Equal(id, userId);
        }
    }
}
