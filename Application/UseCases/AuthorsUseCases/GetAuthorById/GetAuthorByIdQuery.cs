using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class GetAuthorByIdQuery : IRequest<AddAuthorResponse>
    {
        public Guid Id { get; set; }

        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}