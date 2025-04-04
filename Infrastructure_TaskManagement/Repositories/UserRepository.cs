

using Application_TaskManagement.IRepositories;
using Core_TaskManagement.Entities;
using Infrastructure_TaskManagement.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_TaskManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public Task AddAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = _context.Users;  

            return await users.ToListAsync();
        }


        public async Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errorMessages}");
            }
        }

        public async Task UpdateAsync(ApplicationUser entity)
        {
            await _userManager.UpdateAsync(entity);
        }

        async Task<ApplicationUser> IRepository<ApplicationUser>.GetByIdAsync(int id)
        {
            return await _context.Set<ApplicationUser>().FindAsync(id);
        }
    }
}
