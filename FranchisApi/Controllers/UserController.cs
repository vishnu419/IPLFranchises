using FranchisService.Models.Request;
using FranchisService.IService;
using Microsoft.AspNetCore.Mvc;

namespace FranchisApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var success = await _userService.RegisterAsync(registerRequest);
            if (!success) return BadRequest("Username or email already exists");
            return Ok(new { message = "Registration successful. Please login to continue." });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var authResponse = await _userService.LoginAsync(loginRequest);
            if (authResponse == null) return Unauthorized("Invalid credentials");
            return Ok(authResponse);
        }

        /// <summary>
        /// Initiates a password reset process.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(string email)
        {
            // For demo: just return OK. In real app, send email with reset link/token.
            return Ok("If the email exists, a reset link will be sent.");
        }
    }
}