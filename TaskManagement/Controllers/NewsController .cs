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
        //POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsCreateDto dto)
        {
            var news = await _newsService.CreateNews(dto);
            return CreatedAtAction(nameof(GetById), new { id = news.Id }, news);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var news = await _newsService.GetNewsById(id);

                if (news == null)
                    return NotFound(new { message = $"News with Id {id} not found." });

                return Ok(news);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var news = await _newsService.GetLatestNews();
            return Ok(news);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var news = await _newsService.GetNewsByProjectId(projectId);
            return Ok(news);
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
