


namespace Core_TaskManagement.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICollection<Project>? AssignedProjects { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<LogTime>? LogTimes { get; set; }
    }
}
