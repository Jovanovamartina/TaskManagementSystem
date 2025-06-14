using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService userService, UserManager<ApplicationUser> authManager, ILogger<AuthController> logger)
        {
            _authService = userService;
            _userManager = authManager;
            _logger = logger;
        }

        //REGISTER
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto userDto)
        {
            _logger.LogInformation("Received registration request for user: {Email}", userDto.Email);
            await _authService.RegisterUserAsync(userDto);
            _logger.LogInformation("User {Email} registration process completed.", userDto.Email);
            return Ok(new { message = "User registered successfully." });
        }

        //LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           
                var response = await _authService.LoginUserAsync(loginDto);
                return Ok(new { Token = response.Token });
        }
    }
}


