
using Application_TaskManagement.DTOs;

namespace Application_TaskManagement.IServices
{
    public interface INewsService 
    {
        Task<NewsDto> CreateAsync(NewsDto dto);
        Task<NewsDto> UpdateAsync(int id, NewsDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<NewsDto>> GetAllAsync();
        Task<IEnumerable<NewsDto>> GetByProjectIdAsync(int projectId);
    }
}
