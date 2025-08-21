

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
        private readonly IPasswordHasher _passwordHasher;


        public UserService(IRepository<User> userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (existingUser != null)
                throw new Exception("Username already exists.");

            var user = _mapper.Map<User>(dto);

            user.Password = _passwordHasher.Hash(dto.Password);

            await _userRepository.Add(user);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var user = (await _userRepository.GetAll())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (user == null || !_passwordHasher.Verify(user.Password, dto.Password))
                throw new Exception("Invalid username or password.");

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                return null;

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException($"User with Id {id} not found.");

            _mapper.Map(dto, user);

            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                user.Password = _passwordHasher.Hash(dto.NewPassword);
            }

            await _userRepository.Update(user);
        }
        public async Task DeleteAsync(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}

