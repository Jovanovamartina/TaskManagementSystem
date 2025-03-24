

using Application_TaskManagement.DTOs;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterUserAsync(ReisterDto registerUserModel);
    }
}
