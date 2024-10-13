using Application.Exceptions;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.EventUseCases.GetAllUserEvents
{
    public class GetAllUserBooksHandler : IRequestHandler<GetAllUserBooksRequest, GetAllUserBooksResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUserBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllUserBooksResponse> Handle(GetAllUserBooksRequest request, CancellationToken cancellationToken)
        {
            // ��������, ��� id ������������ �� ������ ��� null
            if (string.IsNullOrWhiteSpace(request.id))
            {
                throw new BadRequestException("User ID is required.");
            }

            // �������� ������ ���� ������������
            var res = await _unitOfWork.UserBooks.GetBooksTakenByUserAsync(request.id);

            // ��������, ��� ����� �������
            if (res == null || !res.Any())
            {
                throw new NotFoundException($"No books found for user with ID {request.id}.");
            }

            return new GetAllUserBooksResponse()
            {
                books = res,
            };
        }
    }
}