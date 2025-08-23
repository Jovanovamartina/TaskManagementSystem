
using Application_TaskManagement.DTOs;

namespace Application_TaskManagement.IServices
{
    public interface INewsService 
    {
        Task<NewsDto> CreateNews(NewsCreateDto dto);
        Task<NewsUpdateDto> UpdateNews(int id, NewsUpdateDto dto);
        Task DeleteNews(int id);
        Task<NewsDto> GetNewsById(int id);
        Task<IEnumerable<NewsDto>> GetLatestNews(int count = 5);  
        Task<IEnumerable<NewsDto>> GetNewsByProjectId(int projectId); 
    }
}
