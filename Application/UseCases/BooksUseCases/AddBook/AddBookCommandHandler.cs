using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            book.IsBorrowed = false; // New book is not borrowed by default

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AddBookResponse>(book);
        }
    }
}
