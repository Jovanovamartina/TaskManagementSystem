
using static Core_TaskManagement.Enums.EnumTypes;

namespace Core_TaskManagement.Entities
{
    public class LogTime
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string? Comment { get; set; }
        public ActivityType Activity { get; set; }
        public WorkLocation WorkFrom { get; set; }
    }
}
