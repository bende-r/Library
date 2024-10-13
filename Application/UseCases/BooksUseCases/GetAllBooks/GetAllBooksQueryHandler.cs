using Application.Exceptions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.BooksUseCases.AddBook

{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = _unitOfWork.Books.FindAll().Include(b => b.Author);  // Загрузка автора вместе с книгой

            if (books == null || !books.Any())
            {
                throw new NotFoundException("No books found.");
            }

            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }
    }
}