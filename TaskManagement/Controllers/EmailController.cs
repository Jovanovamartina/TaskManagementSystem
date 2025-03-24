
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UI.Services.IEmailSender _emailSender;

        public EmailController(Microsoft.AspNetCore.Identity.UI.Services.IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendTestEmail(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
            return Ok("Email Sent!");
        }
    }
}
