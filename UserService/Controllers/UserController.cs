using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly Services.UserService _userService;

        public UserController(Services.UserService userService)
        {
            _userService = userService;
        }

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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdate userUpdate)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(id, userUpdate);
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _userService.DeleteUserAsync(id);
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