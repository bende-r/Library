using AutoMapper;

using Domain.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.BooksUseCases.AddBook

{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PaginatedResult<BookResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Books.FindAll()
                                         .Include(b => b.Author) // Включить автора, если нужно
                                         .AsQueryable();

            // Рассчитайте общее количество элементов
            var totalCount = await query.CountAsync(cancellationToken);

            // Примените пагинацию
            var books = await query.Skip((request.Page - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);

            var bookResponses = _mapper.Map<IEnumerable<BookResponse>>(books);

            // Верните пагинированный результат
            return new PaginatedResult<BookResponse>(bookResponses, request.Page, request.PageSize, totalCount);
        }
    }

    //public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookResponse>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<IEnumerable<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    //    {
    //        var books =  _unitOfWork.Books.FindAll().Include(b => b.Author);  // Загрузка автора вместе с книгой
    //        return _mapper.Map<IEnumerable<BookResponse>>(books);
    //    }
    //}
}