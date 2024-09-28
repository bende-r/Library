using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.EventUseCases.GetAllUserEvents;

public class GetAllUserBooksHandler : IRequestHandler<GetAllUserBooksRequest, GetAllUserBooksResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserBooksHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllUserBooksResponse> Handle(GetAllUserBooksRequest request, CancellationToken cancellationToken)
    {
        var res = await _unitOfWork.UserBooks.GetBooksTakenByUserAsync(request.id);
        return new GetAllUserBooksResponse()
        {
            books = res,
        };
    }
}