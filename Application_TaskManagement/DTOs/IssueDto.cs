
using static Core_TaskManagement.Enums.EnumTypes;

namespace Application_TaskManagement.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Author { get; set; }
        public string? Assignee { get; set; }

        public IssueType TaskType { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int PercentageDone { get; set; }
    }
}
