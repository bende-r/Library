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
            var books = await _unitOfWork.Books.GetBooksByAuthorAsync(request.AuthorId);

            if (books == null || !books.Any())
            {
                throw new Exception("No books found for this author");
            }

            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }
    }
}