

using Microsoft.AspNetCore.Identity;

namespace Core_TaskManagement.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Project>? AssignedProjects { get; set; }
        public ICollection<Comment>? Comments { get; set; } 
        public ICollection<LogTime>? LogTimes { get; set; }
    }
}
