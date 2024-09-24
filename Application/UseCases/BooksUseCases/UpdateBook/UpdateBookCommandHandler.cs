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
                throw new Exception("Book not found");
            }

            _mapper.Map(request, book);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateBookResponse>(book);
        }
    }
}
