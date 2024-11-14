using Microsoft.AspNetCore.Mvc;
using DataCompressionAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace DataCompressionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthenticationController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password, string email, bool isAdmin)
        {
            var result = _authService.Register(username, password, email, isAdmin);
            if (result)
            {
                return Ok("Registration successful");
            }
            return BadRequest("Username already exists");
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var token = _authService.Login(username, password);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string username, string newPassword)
        {
            var result = _authService.ResetPassword(username, newPassword);
            if (result)
            {
                return Ok("Password reset successful");
            }
            return BadRequest("User not found");
        }

        [Authorize]
        [HttpDelete("delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var result = _authService.DeleteUser(userId);
            if (result)
            {
                return Ok("User deleted successfully");
            }
            return NotFound("User not found");
        }
    }
}