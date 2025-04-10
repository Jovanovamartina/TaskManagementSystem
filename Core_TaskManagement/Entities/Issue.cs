

using static Core_TaskManagement.Enums.EnumTypes;

namespace Core_TaskManagement.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int ProjectId { get; set; }  
        public Project? Project { get; set; } 
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public string? AssignedUserId { get; set; }
        public ApplicationUser? AssignedUser { get; set; }
        public IssueType TaskType { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }
        public Module Module { get; set; }
        public string? FilePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public double EstimatedTime { get; set; }
        public int PercentageDone { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<LogTime>? LogTimes { get; set; }

    }
}
