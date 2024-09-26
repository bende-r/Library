using Application.Exceptions;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.AuthUseCases.CreateRole;

public class CreateRoleHandler: IRequestHandler<CreateRoleRequest, CreateRoleResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Users.CreateRoleAsync(request.roleName);
        if (!result.Succeeded)
        {
            throw new CreateRoleException(ExceptionMessages.CreateRoleFailed);
        }
        return new CreateRoleResponse()
        {
            Message = "Role created successfully"
        };
    }
}