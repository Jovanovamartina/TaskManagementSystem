

namespace Core_TaskManagement.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsGeneral { get; set; } = true;
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
    }
}
