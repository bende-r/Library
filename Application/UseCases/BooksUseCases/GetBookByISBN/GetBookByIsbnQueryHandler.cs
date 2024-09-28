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
            var book = await _unitOfWork.Books.GetByISBNAsync(request.ISBN);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            return _mapper.Map<BookResponse>(book);
        }
    }
}