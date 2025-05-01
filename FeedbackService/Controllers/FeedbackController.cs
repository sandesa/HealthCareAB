using FeedbackService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : Controller
    {
        private readonly Services.FeedbackService _feedbackService;

        public FeedbackController(Services.FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [EndpointSummary("GET DEV")]
        [EndpointDescription("Get FULL information about all feedbacks (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetFeedbacksDevAsync()
        {
            var response = await _feedbackService.GetFeedbacksDevAsync();

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("GET ALL")]
        [EndpointDescription("Get information about all feedbacks\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackByIdAsync(int id)
        {
            var response = await _feedbackService.GetFeedbackByIdAsync(id);

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [EndpointSummary("GET BY BOOKING ID")]
        [EndpointDescription("Get information about feedback by booking ID\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetFeedbackByBookingIdAsync(int bookingId)
        {
            var response = await _feedbackService.GetFeedbackByBookingIdAsync(bookingId);

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [EndpointSummary("POST FEEDBACK")]
        [EndpointDescription("Create feedback\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateFeedbackAsync([FromBody] FeedbackCreate feedback)
        {
            if (feedback == null)
            {
                return BadRequest("Feedback data is required.");
            }
            var response = await _feedbackService.CreateFeedbackAsync(feedback);

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return CreatedAtAction(null, null, response);
            }
            return BadRequest(response);
        }

        [EndpointSummary("PUT FEEDBACK")]
        [EndpointDescription("Update feedback\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFeedbackAsync(int id, [FromBody] FeedbackUpdate feedback)
        {
            if (feedback == null)
            {
                return BadRequest("Feedback data is required.");
            }
            var response = await _feedbackService.UpdateFeedbackAsync(id, feedback);

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [EndpointSummary("DELETE FEEDBACK")]
        [EndpointDescription("Delete feedback\n\nRequired roles: \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Admin,Developer")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFeedbackAsync(int id)
        {
            var response = await _feedbackService.DeleteFeedbackAsync(id);

            if (response.Message.Contains("error"))
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}