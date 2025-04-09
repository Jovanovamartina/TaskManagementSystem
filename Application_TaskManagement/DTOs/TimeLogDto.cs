
namespace Application_TaskManagement.DTOs
{
    public class TimeLogDto
    {
        public string? ProjectName { get; set; }
        public string? User { get; set; }
        public DateTime Date { get; set; }
        public string? Task { get; set; }
        public string? Activity { get; set; }
        public string? Comment { get; set; }
        public double Hours { get; set; }
    }
}
