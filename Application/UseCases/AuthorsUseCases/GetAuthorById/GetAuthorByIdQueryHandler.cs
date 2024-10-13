using Application.Exceptions;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AddAuthorResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);

            if (author == null)
            {
                throw new NotFoundException($"Author with ID {request.Id} was not found.");
            }

            return _mapper.Map<AddAuthorResponse>(author);
        }
    }
}