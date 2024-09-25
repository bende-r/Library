using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.UserUseCases.AssignRole
{
    public class AssignRoleRequest : IRequest<AssignRoleResponse>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }

}
