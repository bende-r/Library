using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class DeleteAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}