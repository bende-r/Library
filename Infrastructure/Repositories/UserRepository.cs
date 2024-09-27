﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            // Регистрация пользователя
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Назначаем роль "User" новому пользователю
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<ApplicationUser?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == name, cancellationToken);
        }

        public async Task<ApplicationUser?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == token, cancellationToken);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FindAsync(new object[] { userId }, cancellationToken);
        }

    }
}
