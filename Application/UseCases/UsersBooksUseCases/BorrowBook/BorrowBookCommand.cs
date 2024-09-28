using MediatR;

namespace Application.UseCases.UsersBooksUseCases.BorrowBook
{
    public class BorrowBookCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public BorrowBookCommand(string userId, Guid bookId, DateTime borrowDate, DateTime returnDate)
        {
            UserId = userId;
            BookId = bookId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }
    }
}