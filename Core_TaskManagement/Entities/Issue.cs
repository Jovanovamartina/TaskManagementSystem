

using static Core_TaskManagement.Enums.EnumTypes;

namespace Core_TaskManagement.Entities
{
    public class Issue
    {
        public int Id { get; set; } 
        public string? CreatedByUserId { get; set; }  
        public ApplicationUser? CreatedBy { get; set; }
        public IssueType TaskType { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }

        public Module Module { get; set; }

        // Path to the uploaded file (or file data if you're using a file storage mechanism)
        public string FilePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public double EstimatedTime { get; set; }
        public int PercentageDone { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}
