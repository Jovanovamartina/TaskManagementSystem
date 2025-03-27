

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IServices;
using Application_TaskManagement.Token;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace Application_TaskManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, JwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponseDto> LoginUserAsync(LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }

            // Find user by Email
            var existingUser = await _userManager.FindByEmailAsync(loginDto.Username);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Check Password
            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, loginDto.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Generate JWT Token
            var token = _jwtTokenGenerator.GenerateJwtToken(existingUser);

            // Return Token and User Info
            return new AuthResponseDto
            {
                Token = token,
                Email = existingUser.Email,         // ✅ Included
                UserName = existingUser.UserName    // ✅ Included
            };
        }





        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }


        //Register 
        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            if (registerDto == null || string.IsNullOrWhiteSpace(registerDto.Password))
            {
                throw new ArgumentException("User details and password cannot be empty.");
            }

            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var role = "User";

            if (registerDto.Email.EndsWith("@admin.com"))
            {
                role = "Admin";
            }

            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new ArgumentException("Role does not exist.");
            }

            return result;
        }

       
    }
}