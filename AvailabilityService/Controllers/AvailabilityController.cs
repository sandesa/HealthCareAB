using AvailabilityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [EndpointSummary("GET DEV")]
        [EndpointDescription("Get FULL information about all availabilities (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("GET BY CAREGIVER ID")]
        [EndpointDescription("Get information about all availabilities\n\nRequired roles: \"Caregiver, Admin, Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Caregiver, Admin, Developer")]
        [HttpGet("caregiver")]
        public async Task<IActionResult> GetAvailabilitiesByCaregiverId()
        {
            var caregiverId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(caregiverId))
            {
                return BadRequest("Caregiver ID not found.");
            }

            var result = await _availabilityService.GetAvailabilitiesByCaregiverIdAsync(int.Parse(caregiverId));
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

        [EndpointSummary("GET BY DATE")]
        [EndpointDescription("Get information about all availabilities by date\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("GET BY DATE RANGE")]
        [EndpointDescription("Get information about all availabilities by date range. From startdate to \n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("GET BY ID")]
        [EndpointDescription("Get information about availability by ID\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
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

        [EndpointSummary("POST AVAILABILITY")]
        [EndpointDescription("Create new availability\n\nRequired roles: \"Caregiver, Admin, Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Caregiver, Admin, Developer")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAvailability([FromBody] AvailabilityCreate newAvailability)
        {
            var caregiverId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(caregiverId))
            {
                return BadRequest("Caregiver ID not found.");
            }

            var result = await _availabilityService.CreateAvailabilityAsync(newAvailability, int.Parse(caregiverId));
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

        [EndpointSummary("UPDATE AVAILABILITY")]
        [EndpointDescription("Update availability\n\nRequired roles: \"Caregiver, Admin, Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Caregiver, Admin, Developer")]
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

        [EndpointSummary("DELETE AVAILABILITY")]
        [EndpointDescription("Delete availability\n\nRequired roles: \"Caregiver, Admin, Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Caregiver, Admin, Developer")]
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
