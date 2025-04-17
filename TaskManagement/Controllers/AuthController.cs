using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService userService, UserManager<ApplicationUser> authManager)
        {
            _authService = userService;
            _userManager = authManager;
        }

        //HTTTP POST
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto userDto)
        {
           
                await _authService.RegisterUserAsync(userDto);
                return Ok(new { message = "User registered successfully." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           
                var response = await _authService.LoginUserAsync(loginDto);
                return Ok(new { Token = response.Token });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized();

            
                await _authService.ChangePasswordAsync(user, dto);
                return Ok(new { message = "Password changed successfully." });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { message = "Logged out successfully." });
        }
    }
}


