using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.UserUseCases.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleRequest, AssignRoleResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignRoleCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AssignRoleResponse> Handle(AssignRoleRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return new AssignRoleResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "User not found." }
                };
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return new AssignRoleResponse { IsSuccess = true };
            }

            return new AssignRoleResponse
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }

}
