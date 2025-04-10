

using static Core_TaskManagement.Enums.EnumTypes;

namespace Core_TaskManagement.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public ProjectPrioriry Priority { get; set; }
        public ICollection<ApplicationUser>? AssignedTeamMembers { get; set; }
        public ICollection<Issue>? Issues { get; set; }

    }

}
