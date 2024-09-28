using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorResponse>>
    { }
}