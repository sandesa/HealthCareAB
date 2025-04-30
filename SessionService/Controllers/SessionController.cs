using Microsoft.AspNetCore.Authorization;
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

        [EndpointSummary("GET DEV")]
        [EndpointDescription("Get FULL information about all sessions (for developing purposes) \n\nRequired role: \"Developer\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer")]
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

        [EndpointSummary("GET BY ID")]
        [EndpointDescription("Get information about session by ID\n\nRequired roles \"Developer, Admin, Caregiver\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize]
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

        [EndpointSummary("POST SESSION")]
        [EndpointDescription("Create new session\n\nNo required roles\n\nUser must NOT be logged in")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLoginSession([FromBody] SessionCreate sessionCreate)
        {
            try
            {
                var response = await _sessionService.CreateLoginSessionAsync(sessionCreate);
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

        [EndpointSummary("POST LOGOUT")]
        [EndpointDescription("Update session (logout)\n\nNo required roles\n\nUser must NOT be logged in but must provide a valid token")]
        [HttpPut("logout/{token}")]
        public async Task<IActionResult> UpdateSessionLogout(string token)
        {
            try
            {
                var response = await _sessionService.UpdateSessionAsync(token, null, null);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating Session data (logout). Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when updating Session data (logout).");
            }
        }

        [EndpointSummary("PUT SESSION")]
        [EndpointDescription("Update session (login)\n\nRequired roles \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer,Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] SessionUpdate sessionUpdate)
        {
            try
            {
                var response = await _sessionService.UpdateSessionAsync(null, id, sessionUpdate);
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

        [EndpointSummary("DELETE SESSION")]
        [EndpointDescription("Delete session\n\nRequired roles \"Developer, Admin\"\n\nUser must be logged in (have a valid active token)")]
        [Authorize(Roles = "Developer,Admin")]
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