using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Microsoft.AspNetCore.Mvc;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _userService;
       

        public UserController(IAuthService userService)
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

      
    }
}

