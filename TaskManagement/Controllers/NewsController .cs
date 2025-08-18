using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllNews();
            return Ok(news);
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await _newsService.GetNewsByProjectId(id);
            return Ok(news);
        }

        // GET 
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            var news = await _newsService.GetNewsByProjectId(projectId);
            return Ok(news);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsDto dto)
        {
            var news = await _newsService.CreateNews(dto);
            return CreatedAtAction(nameof(GetById), new { id = news.Id }, news);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsDto dto)
        {
            var news = await _newsService.UpdateNews(id, dto);
            return Ok(news);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.DeleteNews(id);
            return NoContent();
        }
    }
}
