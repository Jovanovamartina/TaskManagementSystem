

using Core_TaskManagement.Entities;

namespace Application_TaskManagement.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
