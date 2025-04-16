using FeedbackService.Models;
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