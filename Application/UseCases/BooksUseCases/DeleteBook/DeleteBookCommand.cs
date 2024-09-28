using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}