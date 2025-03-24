

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using AutoMapper;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<string> Authenticate(LoginDto userDto)
        {
            // Step 1: Find user by email
            var user = await _userManager.FindByEmailAsync(userDto.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Step 2: Check if password is correct
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!isPasswordCorrect)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Step 3: Generate JWT token
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplicationUser> RegisterUserAsync(RegisterDto registerDto)
        {
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

                // Creating the user and storing the result of the operation
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                // Now check if result.Succeeded is true
                if (result.Succeeded)
                {
                    // Default role assignment (e.g., "User")
                    var role = "User"; // Default role for all users

                    // Check for special condition to make the user an Admin
                    if (registerDto.Email.EndsWith("@admin.com")) // Example condition (could be different)
                    {
                        role = "Admin"; // Assign Admin role for specific conditions
                    }

                    // Ensure role exists
                    var roleExist = await _roleManager.RoleExistsAsync(role);
                    if (roleExist)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        throw new ArgumentException("Role does not exist.");
                    }
                }
                else
                {
                    // Handle failed registration (result.Errors contains the errors)
                    throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                return user;
            }

        }
    }
}