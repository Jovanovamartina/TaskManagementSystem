using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Microsoft.AspNetCore.Mvc;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _userService;
       

        public AuthenticationController(IAuthService userService)
        {
            _userService = userService;
        }

        //HTTTP POST
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto userDto)
        {
            try
            {
                await _userService.RegisterUserAsync(userDto);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _userService.LoginUserAsync(loginDto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


