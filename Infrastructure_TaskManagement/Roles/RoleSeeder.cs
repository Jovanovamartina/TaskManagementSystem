

using Application_TaskManagement.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure_TaskManagement.Roles
{
    public class RoleSeeder : IRoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRolesAsync()
        {
            string[] roleNames = { "Admin", "User", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}