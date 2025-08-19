

namespace Application_TaskManagement.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public bool IsGeneral { get; set; }
    }
}
