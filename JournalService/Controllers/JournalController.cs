using JournalService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JournalService.Controllers
{
    [ApiController]
    [Route("api/journal")]
    public class JournalController : Controller
    {
        private readonly Services.JournalService _journalService;

        public JournalController(Services.JournalService journalService)
        {
            _journalService = journalService;
        }

        [EndpointSummary("GET DEV")]
        [EndpointDescription("Get FULL information about all journals (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetJournalsDevAsync()
        {
            var response = await _journalService.GetJournalsDevAsync();

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

        [EndpointSummary("GET BY PATIENT ID")]
        [EndpointDescription("Get all journals for user with ID  \n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("user/{patientId}")]
        public async Task<IActionResult> GetJournalsByUserIdAsync(int patientId)
        {
            var response = await _journalService.GetJournalsByUserIdAsync(caregiverId: null, patientId);

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


        [EndpointSummary("GET BY CAREGIVER ID")]
        [EndpointDescription("Get all journals written by caregiver with ID  \n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("caregiver/{caregiverId}")]
        public async Task<IActionResult> GetJournalsByCaregiverIdAsync(int caregiverId)
        {
            var response = await _journalService.GetJournalsByUserIdAsync(caregiverId, patientId: null);

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

        [EndpointSummary("GET BY ID")]
        [EndpointDescription("Get information about journal by ID\n\nNo required roles\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJournalByIdAsync(int id)
        {
            var response = await _journalService.GetJournalByIdAsync(id);

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

        [EndpointSummary("POST JOURNAL")]
        [EndpointDescription("Create journal\n\nRequired roles \"Developer, Admin, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer,Admin,Caregiver")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalAsync([FromBody] JournalCreate journalCreate)
        {
            var response = await _journalService.CreateJournalAsync(journalCreate);

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

        [EndpointSummary("PUT JOURNAL")]
        [EndpointDescription("Update journal\n\nRequired roles \"Developer, Admin, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer,Admin,Caregiver")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJournalAsync(int id, [FromBody] JournalUpdate journalUpdate)
        {
            var response = await _journalService.UpdateJournalAsync(id, journalUpdate);

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

        [EndpointSummary("DELETE JOURNAL")]
        [EndpointDescription("Delete journal\n\nRequired roles \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer,Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJournalAsync(int id)
        {
            var response = await _journalService.DeleteJournalAsync(id);

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
