

using Core_TaskManagement.Entities;

namespace Application_TaskManagement.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, string password);
    }
}
