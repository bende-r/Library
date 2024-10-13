using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByIsbnQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookResponse> Handle(GetBookByIsbnQuery request, CancellationToken cancellationToken)
        {
            // Проверка на валидность ISBN
            if (string.IsNullOrWhiteSpace(request.ISBN))
            {
                throw new BadRequestException("ISBN cannot be empty.");
            }

            // Получение книги по ISBN
            var book = await _unitOfWork.Books.GetByISBNAsync(request.ISBN);

            // Если книга не найдена, бросаем исключение NotFound
            if (book == null)
            {
                throw new NotFoundException($"Book with ISBN {request.ISBN} was not found.");
            }

            // Возвращаем информацию о книге
            return _mapper.Map<BookResponse>(book);
        }
    }
}
