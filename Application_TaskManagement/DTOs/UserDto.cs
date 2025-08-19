

namespace Application_TaskManagement.DTOs
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
