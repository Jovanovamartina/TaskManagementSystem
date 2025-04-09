

namespace Application_TaskManagement.DTOs
{
    public class SpentTimeDto
    {
        public string? ProjectName { get; set; }
        public string? User { get; set; }
        public string? TaskName { get; set; }
        public string? Activity { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
    }
}
