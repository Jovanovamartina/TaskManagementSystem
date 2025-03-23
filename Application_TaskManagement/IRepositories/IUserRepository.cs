

using Core_TaskManagement.Entities;

namespace Application_TaskManagement.IRepositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, string password);
    }
}
