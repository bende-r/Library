using MediatR;

namespace Application.UseCases.AuthUseCases.Register;

public sealed record RegisterRequest : IRequest<RegisterResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}