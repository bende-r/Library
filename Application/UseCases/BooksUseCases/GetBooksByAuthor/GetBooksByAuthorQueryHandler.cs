using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.GetBooksByAuthor
{
    public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, IEnumerable<BookResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksByAuthorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
        {
            // Проверка на валидность AuthorId
            if (request.AuthorId == Guid.Empty)
            {
                throw new BadRequestException("AuthorId cannot be empty.");
            }

            // Получение списка книг автора
            var books = await _unitOfWork.Books.GetBooksByAuthorAsync(request.AuthorId);

            // Если книги не найдены, выбрасываем NotFoundException
            if (books == null || !books.Any())
            {
                throw new NotFoundException($"No books found for author with ID {request.AuthorId}.");
            }

            // Возвращаем книги
            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }
    }
}
