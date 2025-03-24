

namespace Application_TaskManagement.DTOs
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
