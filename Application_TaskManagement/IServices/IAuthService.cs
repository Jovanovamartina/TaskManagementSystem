

using Application_TaskManagement.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Application_TaskManagement.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task LogoutAsync();
    }
}
