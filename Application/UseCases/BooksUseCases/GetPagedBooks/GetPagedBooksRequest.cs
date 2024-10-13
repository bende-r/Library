using MediatR;

namespace Application.UseCases.BooksUseCases.GetPagedBooks
{
    public sealed record GetPagedBooksRequest : IRequest<GetPagedBooksResponse>
    {
        // Максимальный размер страницы
        public const int MaxPageSize = 50;

        // Номер страницы (по умолчанию 1)
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        // Размер страницы (по умолчанию 10)
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // Ограничение на максимальный размер страницы
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
    }
}
