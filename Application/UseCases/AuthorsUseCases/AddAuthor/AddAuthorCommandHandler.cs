using Application.Exceptions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            // Проверка на корректность данных запроса
            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new BadRequestException("First name and last name are required.");
            }

            if (string.IsNullOrWhiteSpace(request.Country))
            {
                throw new BadRequestException("Country is required.");
            }

            if (request.DateOfBirth == null)
            {
                throw new BadRequestException("Date of birth is required.");
            }

            // Проверка, существует ли автор с такими же данными
            var existingAuthor = await _unitOfWork.Authors.FindAsync(a =>
                a.FirstName == request.FirstName &&
                a.LastName == request.LastName &&
                a.Country == request.Country &&
                a.DateOfBirth == request.DateOfBirth);

            if (existingAuthor != null)
            {
                throw new AlreadyExistsException("Author with the same name already exists.");
            }

            // Создание и сохранение нового автора
            var author = _mapper.Map<Author>(request);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AddAuthorResponse>(author);
        }
    }
}