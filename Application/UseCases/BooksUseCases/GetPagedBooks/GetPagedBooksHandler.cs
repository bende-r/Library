using Application.Exceptions;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.GetPagedBooks
{
    public class GetPagedBooksHandler : IRequestHandler<GetPagedBooksRequest, GetPagedBooksResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPagedBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPagedBooksResponse> Handle(GetPagedBooksRequest request, CancellationToken cancellationToken)
        {
            // ѕроверка валидности параметров страницы и размера страницы
            if (request.PageNumber <= 0)
            {
                throw new BadRequestException("Page number must be greater than 0.");
            }

            if (request.PageSize <= 0)
            {
                throw new BadRequestException("Page size must be greater than 0.");
            }

            // ѕолучение книг с постраничной разбивкой
            var books = await _unitOfWork.Books.GetPagedBooksAsync(request.PageNumber, request.PageSize, cancellationToken);

            // ѕроверка на случай, если книг не найдено
            if (books == null || !books.Any())
            {
                throw new NotFoundException("No books found for the given page and size.");
            }

            return new GetPagedBooksResponse
            {
                books = books,
            };
        }
    }
}
