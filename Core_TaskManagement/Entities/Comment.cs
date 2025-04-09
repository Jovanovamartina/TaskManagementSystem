

namespace Core_TaskManagement.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? Author { get; set; }
        public string? CommentText { get; set; }
        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Issue? Task { get; set; }
    }
}
