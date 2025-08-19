

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using AutoMapper;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;


        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (existingUser != null)
                throw new Exception("Username already exists.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Password = dto.Password
            };

            await _userRepository.Add(user);

            return new UserDto
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var user = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);

            if (user == null)
                throw new Exception("Invalid username or password.");

            return new UserDto
            {
                UserID = user!.UserID,
                FirstName = user!.FirstName!,
                LastName = user!.LastName!,
                Username = user!.Username!
            };
        }


        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task UpdateAsync(int id, RegisterDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) throw new KeyNotFoundException();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Username = dto.Username;
            user.Password = dto.Password; 

            await _userRepository.Update(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}

