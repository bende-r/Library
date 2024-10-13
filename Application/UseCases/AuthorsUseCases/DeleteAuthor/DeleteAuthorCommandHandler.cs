using Application.Exceptions;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);

            if (author == null)
            {
                throw new NotFoundException($"Author with ID {request.Id} was not found.");
            }

            await _unitOfWork.Authors.DeleteAsync(author.Id);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}