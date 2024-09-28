using MediatR;

namespace Application.UseCases.UsersBooksUseCases.ReturnBook
{
    public class ReturnBookCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public Guid BookId { get; set; }

        public ReturnBookCommand(string userId, Guid bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}