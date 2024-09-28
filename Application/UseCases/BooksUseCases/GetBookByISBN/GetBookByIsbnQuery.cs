using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIsbnQuery : IRequest<BookResponse>
    {
        public string ISBN { get; set; }

        public GetBookByIsbnQuery(string isbn)
        {
            ISBN = isbn;
        }
    }
}