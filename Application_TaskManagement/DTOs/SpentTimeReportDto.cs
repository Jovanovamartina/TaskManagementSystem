

namespace Application_TaskManagement.DTOs
{
    public class SpentTimeReportDto
    {
        public List<TimeLogDto> Data { get; set; }
        public double TotalHours { get; set; }
    }
}
