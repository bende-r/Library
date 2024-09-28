using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.GetPagedBooks;

public class GetPagedBooksHandler : IRequestHandler<GetPagedBooksRequest, GetPagedBooksResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedBooksHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPagedBooksResponse> Handle(GetPagedBooksRequest request, CancellationToken cancellationToken)
    {
        var res = await _unitOfWork.Books.GetPagedBooksAsync(request.PageNumber, request.PageSize, cancellationToken);

        return new GetPagedBooksResponse()
        {
            books = res,
        };
    }
}