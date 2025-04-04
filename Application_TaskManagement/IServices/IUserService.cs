

using Core_TaskManagement.Entities;

namespace Application_TaskManagement.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<ApplicationUser>> GetUsers();
        public Task<ApplicationUser> GetUser(int id);
    }
}
