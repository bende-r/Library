using Application.Exceptions;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            // Проверяем, существует ли книга
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.Id} was not found.");
            }

            // Удаление книги
            await _unitOfWork.Books.DeleteAsync(book.Id);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
