

using Microsoft.AspNetCore.Identity;

namespace Core_TaskManagement.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
