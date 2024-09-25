using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Domain.Entities;
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
                throw new Exception("Book not found");
            }

            // Проверяем, была ли книга выдана
            if (!book.IsBorrowed)
            {
                throw new Exception("Book is not borrowed");
            }

            // Получаем пользователя
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Получаем запись о выдаче книги пользователю
            var userBook = await _unitOfWork.UserBooks.FindAsync(ub => ub.UserId == request.UserId && ub.BookId == request.BookId, cancellationToken);
            if (userBook == null)
            {
                throw new Exception("Borrow record not found");
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
