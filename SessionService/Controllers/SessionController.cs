using Microsoft.AspNetCore.Mvc;
using SessionService.Models;

namespace SessionService.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : Controller
    {
        private readonly Services.SessionService _sessionService;

        public SessionController(Services.SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("dev")]
        public async Task<IActionResult> GetSessionsDev()
        {
            try
            {
                var response = await _sessionService.GetSessionsDevAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting DEV Session data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when getting DEV Session data.");
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            try
            {
                var response = await _sessionService.GetSessionByIdAsync(id);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting Session data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when getting Session data.");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSession([FromBody] SessionCreate sessionCreate)
        {
            try
            {
                var response = await _sessionService.CreateSessionAsync(sessionCreate);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when creating Session data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when creating Session data.");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] SessionUpdate sessionUpdate)
        {
            try
            {
                var response = await _sessionService.UpdateSessionAsync(id, sessionUpdate);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating Session data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when updating Session data.");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var response = await _sessionService.DeleteSessionAsync(id);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when deleting Session data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when deleting Session data.");
            }
        }
    }
}