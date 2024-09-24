using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);

            if (author == null)
            {
                throw new Exception("Author not found");
            }

            _mapper.Map(request, author);
            await _unitOfWork.Authors.UpdateAsync(author);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateAuthorResponse>(author);
        }
    }
}
