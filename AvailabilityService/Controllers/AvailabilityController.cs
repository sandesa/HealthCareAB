using AvailabilityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvailabilityService.Controllers
{
    [ApiController]
    [Route("api/availability")]
    public class AvailabilityController : Controller
    {
        private readonly Services.AvailabilityService _availabilityService;

        public AvailabilityController(Services.AvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [Authorize(Roles = "Developer")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetDevAvailability()
        {
            var result = await _availabilityService.GetAvailabilitiesDevAsync();
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("caregiver/{caregiverId}")]
        public async Task<IActionResult> GetAvailabilitiesByCaregiverId(int caregiverId)
        {
            var result = await _availabilityService.GetAvailabilitiesByCaregiverIdAsync(caregiverId);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetAvailabilitiesByDate(string date)
        {
            var result = await _availabilityService.GetAvailabilitiesByDateIdAsync(date);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("get/from/{startDate}")]
        public async Task<IActionResult> GetAvailabilitiesOneMonthFromNow(string startDate)
        {
            var result = await _availabilityService.GetAvailabilitiesOneMonthFromNow(startDate);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAvailabilityById(int id)
        {
            var result = await _availabilityService.GetAvailabilityByIdAsync(id);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAvailability([FromBody] AvailabilityCreate newAvailability)
        {
            var result = await _availabilityService.CreateAvailabilityAsync(newAvailability);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return CreatedAtAction(null, null, result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] AvailabilityUpdate availabilityUpdate)
        {
            var result = await _availabilityService.UpdateAvailabilityAsync(id, availabilityUpdate);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var result = await _availabilityService.DeleteAvailabilityAsync(id);
            if (result.Message.Contains("error"))
            {
                return StatusCode(500, result);
            }
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
