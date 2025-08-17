

using static Core_TaskManagement.Enums.EnumTypes;

namespace Application_TaskManagement.DTOs
{
    public class FilterDto
    {
        public IssueStatus? TaskStatus { get; set; }  
        public IssueType? Tracker { get; set; }  
        public IssuePriority? Priority { get; set; } 
        public string? Author { get; set; }  
        public string? Assignee { get; set; }  
        public string? Subject { get; set; } 
        public string? Description { get; set; }  
        public int? PercentageDone { get; set; }  
        public Module Module { get; set; }  
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }  
        public double? EstimatedTime { get; set; } 
        public double? SpentTime { get; set; }  
    }
}
