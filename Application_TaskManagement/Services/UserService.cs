

using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, string password)
        {
            return await _userRepository.RegisterUserAsync(user, password);
        }
    }
}
