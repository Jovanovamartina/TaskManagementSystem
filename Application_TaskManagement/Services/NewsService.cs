

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using AutoMapper;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;
        public NewsService(IRepository<News> newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<NewsDto> CreateAsync(NewsDto dto)
        {
            var news = _mapper.Map<News>(dto);
            news.CreatedAt = DateTime.UtcNow;

            await _newsRepository.Add(news);
            return _mapper.Map<NewsDto>(news);
        }

        public async Task DeleteAsync(int id)
        {
            var news = await _newsRepository.GetById(id);

            if (news == null)
                throw new KeyNotFoundException($"News with Id {id} was not found.");

            await _newsRepository.Delete(id);
        }

        public async Task<IEnumerable<NewsDto>> GetAllAsync()
        {
            var newList = await _newsRepository.GetAll();
            return _mapper.Map<IEnumerable<NewsDto>>(newList);
        }

        public async Task<IEnumerable<NewsDto>> GetByProjectIdAsync(int projectId)
        {
            var allNews = await _newsRepository.GetAll();

            var filteredNews = allNews.Where(n => n.ProjectId == projectId);

            return _mapper.Map<IEnumerable<NewsDto>>(filteredNews);
        }

        public async Task<NewsDto> UpdateAsync(int id, NewsDto dto)
        {
            var existingNews = await _newsRepository.GetById(id);
            if (existingNews == null)
                throw new KeyNotFoundException($"News with Id {id} was not found.");

            _mapper.Map(dto, existingNews);

            await _newsRepository.Update(existingNews);

            return _mapper.Map<NewsDto>(existingNews);
        }
    }
}

