using JournalService;
using JournalService.Models;
using JournalService.Utilities.StdDef;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class JournalServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public JournalServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            var token = JwtTokenGeneratorTest.GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetJournalsDev_ReturnsOk_WhenIsSuccess()
        {
            var response = await _client.GetAsync("/api/journal/dev");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var journals = jsonResponse.GetProperty("data");

            Assert.NotEmpty(journals.EnumerateArray());
        }

        [Fact]
        public async Task GetJournalsByCaregiverId_ReturnsOk_WhenIsSuccess()
        {
            int caregiverId = 2;

            var response = await _client.GetAsync($"/api/journal/caregiver/{caregiverId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var journals = jsonResponse.GetProperty("data");

            Assert.NotEmpty(journals.EnumerateArray());
        }

        [Fact]
        public async Task GetJournalsByPatientId_ReturnsOk_WhenIsSuccess()
        {
            int patientId = 1;

            var response = await _client.GetAsync($"/api/journal/user/{patientId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var journals = jsonResponse.GetProperty("data");

            Assert.NotEmpty(journals.EnumerateArray());
        }

        [Fact]
        public async Task GetJournalById_ReturnsOk_WhenIsSuccess()
        {
            int patientId = 1;

            var response = await _client.GetAsync($"/api/journal/{patientId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var journal = jsonResponse.GetProperty("data");

            Assert.Equal(1, journal.GetProperty("patientId").GetInt32());
        }

        [Fact]
        public async Task CreateJournal_ReturnsOk_WhenCreated()
        {
            JournalCreate journal = new()
            {
                PatientId = 10,
                CaregiverId = 4,
                BookingId = 16,
                JournalType = JournalType.Treatment.ToString(),
                JournalEntry = "Test journal entry",
            };

            var response = await _client.PostAsJsonAsync($"/api/journal/create", journal);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdJournal = jsonResponse.GetProperty("data");

            Assert.Equal(10, createdJournal.GetProperty("patientId").GetInt32());
            Assert.Equal(4, createdJournal.GetProperty("caregiverId").GetInt32());
            Assert.Equal(16, createdJournal.GetProperty("bookingId").GetInt32());
            Assert.Equal(JournalType.Treatment.ToString(), createdJournal.GetProperty("journalType").GetString());
            Assert.Equal("Test journal entry", createdJournal.GetProperty("journalEntry").GetString());
            Assert.Equal(DateTime.Now.ToShortDateString(), createdJournal.GetProperty("createdAt").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task UpdateJournal_ReturnsOk_WhenUpdated()
        {
            int journalId = 1;

            JournalUpdate updateJournal = new()
            {
                BookingId = 20,
                JournalType = JournalType.Other.ToString(),
                JournalEntry = "Updated journal entry",
            };
            var response = await _client.PutAsJsonAsync($"/api/journal/update/{journalId}", updateJournal);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var updatedJournal = jsonResponse.GetProperty("data");

            Assert.Equal(20, updatedJournal.GetProperty("bookingId").GetInt32());
            Assert.Equal(JournalType.Other.ToString(), updatedJournal.GetProperty("journalType").GetString());
            Assert.Equal("Updated journal entry", updatedJournal.GetProperty("journalEntry").GetString());
            Assert.Equal(DateTime.Now.ToShortDateString(), updatedJournal.GetProperty("updatedAt").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task DeleteJournal_ReturnsOk_WhenDeleted()
        {
            int journalId = 4;

            var response = await _client.DeleteAsync($"/api/journal/delete/{journalId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

            Assert.Equal("Journal deleted successfully.", jsonResponse.GetProperty("message").ToString());
            Assert.True(jsonResponse.GetProperty("isSuccess").GetBoolean());
        }
    }
}
