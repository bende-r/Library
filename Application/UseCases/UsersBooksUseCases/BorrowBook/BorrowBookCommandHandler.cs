using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("Book not found");
            }

            // Проверяем, доступна ли книга
            if (book.IsBorrowed)
            {
                throw new Exception("Book is already borrowed");
            }

            // Получаем пользователя
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
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
