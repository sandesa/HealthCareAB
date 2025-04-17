using JournalService.Models;
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
