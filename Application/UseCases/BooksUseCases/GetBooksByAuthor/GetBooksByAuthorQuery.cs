using MediatR;

namespace Application.UseCases.BooksUseCases.GetBooksByAuthor
{
    public class GetBooksByAuthorQuery : IRequest<IEnumerable<BookResponse>>
    {
        public Guid AuthorId { get; set; }

        public GetBooksByAuthorQuery(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}