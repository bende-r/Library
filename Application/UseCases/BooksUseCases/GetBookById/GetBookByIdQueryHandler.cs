using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            // Проверка, что ID книги указан и корректен
            if (request.Id == Guid.Empty)
            {
                throw new BadRequestException("Book ID cannot be empty.");
            }

            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);

            // Если книга не найдена, бросаем исключение NotFound
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.Id} was not found.");
            }

            // Маппим книгу на объект ответа
            return _mapper.Map<BookResponse>(book);
        }
    }
}