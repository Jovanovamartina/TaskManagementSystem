
using Application_TaskManagement.DTOs;

namespace Application_TaskManagement.IServices
{
    public interface INewsService 
    {
        Task<NewsDto> CreateNews(NewsDto dto);
        Task<NewsDto> UpdateNews(int id, NewsDto dto);
        Task DeleteNews(int id);
        Task<IEnumerable<NewsDto>> GetAllNews();
        Task<NewsDto> GetNewsById(int id);
        Task<IEnumerable<NewsDto>> GetNewsByProjectId(int projectId);
    }
}
