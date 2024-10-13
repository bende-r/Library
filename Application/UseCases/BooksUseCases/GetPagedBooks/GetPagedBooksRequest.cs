using MediatR;

namespace Application.UseCases.BooksUseCases.GetPagedBooks
{
    public sealed record GetPagedBooksRequest : IRequest<GetPagedBooksResponse>
    {
        // ������������ ������ ��������
        public const int MaxPageSize = 50;

        // ����� �������� (�� ��������� 1)
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        // ������ �������� (�� ��������� 10)
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // ����������� �� ������������ ������ ��������
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
    }
}
