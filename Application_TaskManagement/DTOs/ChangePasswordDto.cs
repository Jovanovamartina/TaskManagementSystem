

namespace Application_TaskManagement.DTOs
{
    public class ChangePasswordDto
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? RepeatNewPassword { get; set; }

    }

}
