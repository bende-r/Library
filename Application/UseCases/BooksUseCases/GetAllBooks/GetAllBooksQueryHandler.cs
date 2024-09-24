using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Domain.Interfaces;
using MediatR;

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
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }
    }
}
