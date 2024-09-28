using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIdQuery : IRequest<BookResponse>
    {
        public Guid Id { get; set; }

        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}