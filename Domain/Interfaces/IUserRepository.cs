﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task AddToRoleAsync(ApplicationUser user, string roleName);
        Task<ApplicationUser?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken);
    }
}
