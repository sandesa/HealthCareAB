using LoginService.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly Services.LoginService _loginService;

        public LoginController(Services.LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var validationResponse = await _loginService.ValidateUserAsync(request);

                if (validationResponse == null || !validationResponse.IsValid)
                {
                    return Unauthorized(validationResponse);
                }

                if (!validationResponse.IsValid)
                {
                    return Unauthorized(validationResponse);
                }

                var loginResponse = await _loginService.LoginUserAsync(validationResponse);

                if (!loginResponse.IsLoginSuccessful)
                {
                    return StatusCode(500, loginResponse);
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in the controller when logging in. Error message: \"{ex.Message}\"");
                return StatusCode(500, "An error occurred when logging in.");
            }
        }
    }
}
