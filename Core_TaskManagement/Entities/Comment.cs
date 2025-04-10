

namespace Core_TaskManagement.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int IssueId { get; set; }           
        public Issue? Issue { get; set; }          
        public string? AuthorId { get; set; }       
        public ApplicationUser? Author { get; set; } 
        public string? CommentText { get; set; }
        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
