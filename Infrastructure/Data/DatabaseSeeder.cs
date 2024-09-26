using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // Создание ролей
            await CreateRoleAsync("Admin");
            await CreateRoleAsync("User");

            // Создание администратора
            await CreateUserAsync("admin@library.com", "Admin123!", "Admin");

            // Создание обычного пользователя
            await CreateUserAsync("user@library.com", "User123!", "User");
        }

        private async Task CreateRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task CreateUserAsync(string email, string password, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
