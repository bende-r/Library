using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.UsersBooksUseCases.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReturnBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            // Получаем книгу
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            // Проверяем, была ли книга выдана
            if (!book.IsBorrowed)
            {
                throw new BadRequestException("Book is not borrowed.");
            }

            // Получаем пользователя
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {request.UserId} was not found.");
            }

            // Получаем запись о выдаче книги пользователю
            var userBook = await _unitOfWork.UserBooks.FindAsync(ub => ub.UserId == request.UserId && ub.BookId == request.BookId, cancellationToken);
            if (userBook == null)
            {
                throw new NotFoundException("Borrow record not found.");
            }

            // Обновляем статус книги на "доступна"
            book.IsBorrowed = false;

            // Удаляем запись о выдаче книги
            await _unitOfWork.UserBooks.DeleteAsync(userBook.Id);

            // Обновляем книгу в базе
            await _unitOfWork.Books.UpdateAsync(book);

            // Сохраняем изменения
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
