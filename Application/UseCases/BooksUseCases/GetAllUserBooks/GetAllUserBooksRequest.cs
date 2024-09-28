using MediatR;

namespace Application.UseCases.EventUseCases.GetAllUserEvents;

public sealed record GetAllUserBooksRequest(string id) : IRequest<GetAllUserBooksResponse>
{
}