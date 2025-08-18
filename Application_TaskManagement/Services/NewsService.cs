

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
        public async Task<NewsDto> CreateNews(NewsDto dto)
        {
            var news = _mapper.Map<News>(dto);
            news.CreatedAt = DateTime.UtcNow;

            await _newsRepository.Add(news);
            return _mapper.Map<NewsDto>(news);
        }

        public async Task DeleteNews(int id)
        {
            var news = await _newsRepository.GetById(id);

            if (news == null)
                throw new KeyNotFoundException($"News with Id {id} was not found.");

            await _newsRepository.Delete(id);
        }

        public async Task<IEnumerable<NewsDto>> GetLatestNews(int count = 5)
        {
            var allNews = await _newsRepository.GetAll();

            var generalNews = allNews
                .Where(n => n.IsGeneral)
                .OrderByDescending(n => n.CreatedAt) 
                .Take(count);                        

            return _mapper.Map<IEnumerable<NewsDto>>(generalNews);
        }

        public async Task<NewsDto> GetNewsById(int id)
        {
            var news = await _newsRepository.GetById(id);
            if (news == null)
                throw new KeyNotFoundException($"News with Id {id} was not found.");

            return _mapper.Map<NewsDto>(news);
        }

        // Get all news by ProjectId
        public async Task<IEnumerable<NewsDto>> GetNewsByProjectId(int projectId)
        {
            var allNews = await _newsRepository.GetAll();

            var projectNews = allNews
                .Where(n => !n.IsGeneral && n.ProjectId == projectId)
                .OrderByDescending(n => n.CreatedAt);

            return _mapper.Map<IEnumerable<NewsDto>>(projectNews);
        }


        public async Task<NewsDto> UpdateNews(int id, NewsDto dto)
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

