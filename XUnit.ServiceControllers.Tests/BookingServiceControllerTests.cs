using BookingService;
using BookingService.Models;
using BookingService.Utilities.StdDef;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace XUnit.ServiceControllers.Tests
{
    public class BookingServiceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public BookingServiceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBookingsDev_ReturnsOk_WhenIsSuccess()
        {
            int numberOfBookings = 7;

            var response = await _client.GetAsync("/api/booking/dev");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var bookings = jsonResponse.GetProperty("data");

            Assert.NotEmpty(bookings.EnumerateArray());
            Assert.Equal(numberOfBookings, bookings.GetArrayLength());
        }

        [Fact]
        public async Task GetBookingsByCaregiverId_ReturnsOk_WhenIsSuccess()
        {
            int caregiverId = 1;
            int numberOfBookings = 5;

            var response = await _client.GetAsync($"/api/booking/caregiver/{caregiverId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var bookings = jsonResponse.GetProperty("data");

            Assert.NotEmpty(bookings.EnumerateArray());
            Assert.Equal(numberOfBookings, bookings.GetArrayLength());
        }

        [Fact]
        public async Task GetBookingsByPatientId_ReturnsOk_WhenIsSuccess()
        {
            int patientId = 1;
            int numberOfBookings = 2;

            var response = await _client.GetAsync($"/api/booking/user/{patientId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var bookings = jsonResponse.GetProperty("data");

            Assert.NotEmpty(bookings.EnumerateArray());
            Assert.Equal(numberOfBookings, bookings.GetArrayLength());
        }

        [Fact]
        public async Task GetBookingById_ReturnsOk_WhenIsSuccess()
        {
            int bookingId = 1;

            var response = await _client.GetAsync($"/api/booking/{bookingId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var booking = jsonResponse.GetProperty("data");

            Assert.Equal(bookingId, booking.GetProperty("id").GetInt32());
        }

        [Fact]
        public async Task GetBookingById_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            int bookingId = 9999;

            var response = await _client.GetAsync($"/api/booking/{bookingId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var errorMessage = jsonResponse.GetProperty("message").GetString();

            Assert.Equal("No booking found with this ID: 9999", errorMessage);
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenCreated()
        {
            BookingCreation newBooking = new()
            {
                CaregiverId = 1,
                PatientId = 2,
                MeetingDate = new DateTime(2025, 04, 27),
                MeetingType = MeetingType.InitialConsultation.ToString(),
                Clinic = "Main Clinic",
                Address = "Testgatan 1, 41234 Gothenburg",
            };

            var response = await _client.PostAsJsonAsync($"/api/booking/create", newBooking);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var createdBooking = jsonResponse.GetProperty("data");

            Assert.Equal(1, createdBooking.GetProperty("caregiverId").GetInt32());
            Assert.Equal(2, createdBooking.GetProperty("patientId").GetInt32());
            Assert.Equal("2025-04-27T00:00:00", createdBooking.GetProperty("meetingDate").GetString());
            Assert.Equal(MeetingType.InitialConsultation.ToString(), createdBooking.GetProperty("meetingType").GetString());
            Assert.Equal("Main Clinic", createdBooking.GetProperty("clinic").GetString());
            Assert.Equal("Testgatan 1, 41234 Gothenburg", createdBooking.GetProperty("address").GetString());
            Assert.False(createdBooking.GetProperty("isCancelled").GetBoolean());
            Assert.Equal(DateTime.Now.ToShortDateString(), createdBooking.GetProperty("created").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task UpdateBooking_ReturnsOk_WhenUpdated()
        {
            int bookingId = 1;
            BookingUpdate updatedBooking = new()
            {
                MeetingDate = new DateTime(2025, 06, 02),
                MeetingType = MeetingType.Other.ToString(),
                Clinic = "Test Clinic",
                Address = "NewTestgatan 1, 41234 Gothenburg",
            };

            var response = await _client.PutAsJsonAsync($"/api/booking/update/{bookingId}", updatedBooking);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var updatedBookingResponse = jsonResponse.GetProperty("data");

            Assert.Equal(bookingId, updatedBookingResponse.GetProperty("id").GetInt32());
            Assert.Equal(1, updatedBookingResponse.GetProperty("caregiverId").GetInt32());
            Assert.Equal(1, updatedBookingResponse.GetProperty("patientId").GetInt32());
            Assert.Equal("2025-06-02T00:00:00", updatedBookingResponse.GetProperty("meetingDate").GetString());
            Assert.Equal(MeetingType.Other.ToString(), updatedBookingResponse.GetProperty("meetingType").GetString());
            Assert.Equal("Test Clinic", updatedBookingResponse.GetProperty("clinic").GetString());
            Assert.Equal("NewTestgatan 1, 41234 Gothenburg", updatedBookingResponse.GetProperty("address").GetString());
            Assert.False(updatedBookingResponse.GetProperty("isCancelled").GetBoolean());
            Assert.Equal(DateTime.Now.ToShortDateString(), updatedBookingResponse.GetProperty("updated").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task CancelBooking_ReturnsOk_WhenCancelled()
        {
            int bookingId = 1;

            var response = await _client.PutAsJsonAsync($"/api/booking/cancel/{bookingId}", bookingId);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var cancelledBooking = jsonResponse.GetProperty("data");

            Assert.Equal(bookingId, cancelledBooking.GetProperty("id").GetInt32());
            Assert.Equal(1, cancelledBooking.GetProperty("caregiverId").GetInt32());
            Assert.Equal(1, cancelledBooking.GetProperty("patientId").GetInt32());
            Assert.True(cancelledBooking.GetProperty("isCancelled").GetBoolean());
            Assert.Equal(DateTime.Now.ToShortDateString(), cancelledBooking.GetProperty("cancelDate").GetDateTime().ToShortDateString());
        }

        [Fact]
        public async Task DeleteBooking_ReturnsOk_WhenCancelled()
        {
            int bookingId = 1;

            var response = await _client.DeleteAsync($"/api/booking/delete/{bookingId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var deletedBooking = jsonResponse.GetProperty("message");

            Assert.Equal("Booking deleted successfully.", deletedBooking.GetString());
        }
    }
}
