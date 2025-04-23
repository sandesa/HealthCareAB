using BookingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpGet("caregiver/{caregiverId}")]
        public async Task<IActionResult> GetBookingsByCaregiverId(int caregiverId)
        {
            var response = await _bookingService.GetBookingsByUserIdAsync(caregiverId: caregiverId, patientId: null);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("user/{patientId}")]
        public async Task<IActionResult> GetBookingsByPatientId(int patientId)
        {
            var response = await _bookingService.GetBookingsByUserIdAsync(caregiverId: null, patientId: patientId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

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

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreation bookingCreation)
        {
            var response = await _bookingService.CreateBookingAsync(bookingCreation);
            if (response.IsSuccess)
            {
                return CreatedAtAction(null, null, response);
            }
            return BadRequest(response);
        }

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
