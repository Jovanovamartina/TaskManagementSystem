﻿

namespace Application_TaskManagement.DTOs
{
    public class TaskReminderDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
