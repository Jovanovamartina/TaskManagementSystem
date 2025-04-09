

using Microsoft.AspNetCore.Http;

namespace Application_TaskManagement.DTOs
{
    public class CommentDto
    {
        public string? CommentText { get; set; }
        public IFormFile? File { get; set; }
    }
}
