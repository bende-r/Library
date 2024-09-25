using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.UserUseCases.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));

            if (result.Succeeded)
            {
                return new CreateRoleResponse { IsSuccess = true };
            }

            return new CreateRoleResponse
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }

}
