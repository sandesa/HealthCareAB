using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly Services.UserService _userService;

        public UserController(Services.UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Developer")]
        [HttpGet("dev")]
        public async Task<IActionResult> GetUsersDev()
        {
            try
            {
                var response = await _userService.GetUsersDevAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting DEV User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when getting DEV User data.");
            }
        }

        [Authorize(Roles = "Developer,Admin,Caregiver")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _userService.GetUsersAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when getting User data.");
            }
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetUserById()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found.");
                }

                var response = await _userService.GetUserByIdAsync(int.Parse(userId));
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when getting User data.");
            }
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreation userCreation)
        {
            try
            {
                var response = await _userService.CreateUserAsync(userCreation);
                if (response.IsSuccess)
                {
                    return CreatedAtAction(null, null, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when creating User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when creating User data.");
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found.");
                }

                var response = await _userService.UpdateUserAsync(int.Parse(userId), userUpdate);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when updating User data.");
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found.");
                }

                var response = await _userService.DeleteUserAsync(int.Parse(userId));
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when deleting User data. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when deleting User data.");
            }
        }

        [AllowAnonymous]
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateUser([FromBody] ValidationRequest validationRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(validationRequest.Email) || string.IsNullOrEmpty(validationRequest.Password))
                {
                    return BadRequest("Not a valid request.");
                }

                var validation = await _userService.ValidateUserAsync(validationRequest.Email, validationRequest.Password);

                if (validation.IsSuccess)
                {
                    return Ok(validation);
                }

                return Unauthorized(validation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when validating user. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when validating user.");
            }
        }
    }
}