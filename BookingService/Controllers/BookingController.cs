using BookingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly Services.BookingService _bookingService;

        public BookingController(Services.BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [EndpointSummary("GET DEV")]
        [EndpointDescription("Get FULL information about all bookings (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetBookingsDev()
        {
            var response = await _bookingService.GetBookingsDevAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("GET BY CAREGIVER ID")]
        [EndpointDescription("Get information about all bookings with caregiver ID\n\nRequired roles: \"Developer, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Caregiver, Developer")]
        [HttpGet("caregiver")]
        public async Task<IActionResult> GetBookingsByCaregiverId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            var response = await _bookingService.GetBookingsByUserIdAsync(caregiverId: int.Parse(userId), patientId: null);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("GET BY USER ID")]
        [EndpointDescription("Get all bookings for patient with ID\n\nRequired roles: \"User, Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "User, Developer, Admin")]
        [HttpGet("user")]
        public async Task<IActionResult> GetBookingsByPatientId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            var response = await _bookingService.GetBookingsByUserIdAsync(caregiverId: null, patientId: int.Parse(userId));
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("GET BY ID")]
        [EndpointDescription("Get information about booking by ID\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var response = await _bookingService.GetBookingByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [EndpointSummary("POST BOOKING")]
        [EndpointDescription("Create new booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreation bookingCreation)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var response = await _bookingService.CreateBookingAsync(bookingCreation, int.Parse(userId));
            if (response.IsSuccess)
            {
                return CreatedAtAction(null, null, response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("PUT BOOKING")]
        [EndpointDescription("Update booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdate bookingUpdate)
        {
            var response = await _bookingService.UpdateBookingAsync(id, bookingUpdate, false);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("PUT CANCEL BOOKING")]
        [EndpointDescription("Cancel booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var response = await _bookingService.UpdateBookingAsync(id, null, true);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("DELETE BOOKING")]
        [EndpointDescription("Delete booking\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _bookingService.DeleteBookingAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
