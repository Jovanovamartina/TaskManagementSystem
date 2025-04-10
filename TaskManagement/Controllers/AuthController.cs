using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Microsoft.AspNetCore.Mvc;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;
       

        public AuthController(IAuthService userService)
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
                // Return a JSON response for success
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                // Return a JSON response for errors
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _userService.LoginUserAsync(loginDto);
                return Ok(new { Token = response.Token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


