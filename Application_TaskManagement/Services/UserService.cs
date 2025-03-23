

using Application_TaskManagement.DTOs;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using AutoMapper;
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application_TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> RegisterUserAsync(RegisterUserModel registerDto)
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