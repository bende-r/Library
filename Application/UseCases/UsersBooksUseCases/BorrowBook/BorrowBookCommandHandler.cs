using Application.Exceptions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.UsersBooksUseCases.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            // Получаем книгу
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            // Проверяем, доступна ли книга
            if (book.IsBorrowed)
            {
                throw new AlreadyExistsException("Book is already borrowed.");
            }

            // Проверяем наличие пользователя
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {request.UserId} was not found.");
            }

            // Проверка корректности даты взятия и возврата книги
            if (request.BorrowDate == null || request.ReturnDate == null)
            {
                throw new BadRequestException("BorrowDate and ReturnDate are required.");
            }

            if (request.ReturnDate <= request.BorrowDate)
            {
                throw new BadRequestException("ReturnDate must be later than BorrowDate.");
            }

            // Создаем связь между пользователем и книгой (UserBook)
            var userBook = new UserBook
            {
                UserId = request.UserId,
                BookId = request.BookId,
                BorrowedDate = request.BorrowDate,
                ReturnDate = request.ReturnDate
            };

            // Обновляем статус книги на "выдана"
            book.IsBorrowed = true;

            // Сохраняем запись о выдаче книги
            await _unitOfWork.UserBooks.AddAsync(userBook);
            await _unitOfWork.Books.UpdateAsync(book);

            // Сохраняем изменения
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
