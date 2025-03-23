

using Application_TaskManagement.DTOs;
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

        public async Task<ApplicationUser> RegisterUserAsync(RegisterUserModel registerDto)
        {
            if (registerDto == null || string.IsNullOrWhiteSpace(registerDto.Password))
            {
                throw new ArgumentException("User details and password cannot be empty.");
            }

            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            return await _userRepository.RegisterUserAsync(user, registerDto.Password);
        }

    }
}
