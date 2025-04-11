

using Application_TaskManagement.DTOs;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application_TaskManagement.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto loginDto);
        Task ChangePasswordAsync(ApplicationUser user, ChangePasswordDto dto);
        Task LogoutAsync();
    }
}
