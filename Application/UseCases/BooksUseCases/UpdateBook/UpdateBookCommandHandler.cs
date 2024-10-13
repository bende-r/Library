using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);

            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.Id} was not found.");
            }

            // Проверка на обязательные поля
            if (string.IsNullOrWhiteSpace(request.ISBN))
            {
                throw new BadRequestException("ISBN is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadRequestException("Title is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Genre))
            {
                throw new BadRequestException("Genre is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new BadRequestException("Description is required.");
            }

            if (request.AuthorId == Guid.Empty)
            {
                throw new BadRequestException("AuthorId is required.");
            }

            // Проверка на уникальность ISBN, если ISBN был изменен
            if (!string.IsNullOrEmpty(request.ISBN) && request.ISBN != book.ISBN)
            {
                var existingBookWithSameISBN = await _unitOfWork.Books.GetByISBNAsync(request.ISBN);
                if (existingBookWithSameISBN != null)
                {
                    throw new AlreadyExistsException("A book with the same ISBN already exists.");
                }
            }

            // Обновление книги
            _mapper.Map(request, book);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateBookResponse>(book);
        }
    }
}
