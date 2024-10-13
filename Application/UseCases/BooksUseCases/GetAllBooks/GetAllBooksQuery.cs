using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookResponse>>
    {
    }
}