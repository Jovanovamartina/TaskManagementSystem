
using Application_TaskManagement.DTOs;

namespace Application_TaskManagement.IServices
{
    public interface INewsService 
    {
        Task<NewsDto> CreateNews(NewsDto dto);
        Task<NewsDto> UpdateNews(int id, NewsDto dto);
        Task DeleteNews(int id);
        Task<NewsDto> GetNewsById(int id);
        Task<IEnumerable<NewsDto>> GetLatestNews(int count = 5); 
        Task<IEnumerable<NewsDto>> GetNewsByProjectId(int projectId); 
    }
}
