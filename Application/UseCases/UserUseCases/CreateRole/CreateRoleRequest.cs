using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.UserUseCases.CreateRole
{
    public class CreateRoleRequest : IRequest<CreateRoleResponse>
    {
        public string RoleName { get; set; }

        public CreateRoleRequest(string roleName)
        {
            RoleName = roleName;
        }
    }

}
