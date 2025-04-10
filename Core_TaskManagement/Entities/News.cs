

namespace Core_TaskManagement.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
    }
}
