

using Microsoft.AspNetCore.Identity;

namespace Core_TaskManagement.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }

    }
}
