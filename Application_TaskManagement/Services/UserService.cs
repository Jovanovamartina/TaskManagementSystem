

using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _useRepository;
        public UserService(IRepository<ApplicationUser> useRepository)
        {
            _useRepository = useRepository; 
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _useRepository.GetAllAsync();
        }
    }
}
