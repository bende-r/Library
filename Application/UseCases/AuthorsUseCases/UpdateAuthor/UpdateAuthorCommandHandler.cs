using Application.Exceptions;

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
            // Проверка на существование автора
            var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {request.Id} was not found.");
            }

            // Проверка на дубликаты
            var existingAuthor = await _unitOfWork.Authors.FindAsync(a =>
                a.FirstName == request.FirstName &&
                a.LastName == request.LastName &&
                a.Country == request.Country &&
                a.DateOfBirth == request.DateOfBirth &&
                a.Id != request.Id); // Исключаем текущего автора

            if (existingAuthor != null)
            {
                throw new AlreadyExistsException("Author with the same name and details already exists.");
            }

            // Обновление данных автора
            _mapper.Map(request, author);
            await _unitOfWork.Authors.UpdateAsync(author);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateAuthorResponse>(author);
        }
    }
}
