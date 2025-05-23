﻿using LoginService.Models;
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

        [EndpointSummary("POST LOGIN")]
        [EndpointDescription("Login user\n\nNo role required\n\nUser must NOT be logged in")]
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

        [EndpointSummary("POST LOGOUT")]
        [EndpointDescription("Logout user\n\nNo role required\n\nUser must be logged in (have a valid active token)")]
        [HttpPost("logout/{token}")]
        public async Task<IActionResult> Logout(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { Message = "Missing or invalid token." });
                }

                var response = await _loginService.LogoutAsync(token);
                if (response.IsLogoutSuccessful)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, IsConnectedToService = false });
            }
        }
    }
}
