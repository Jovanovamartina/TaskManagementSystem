

namespace Core_TaskManagement.Entities
{
    public class TaskReminder
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }


}
