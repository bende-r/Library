using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetAllBooksQuery : IRequest<PaginatedResult<BookResponse>>
    {
        public int Page { get; }
        public int PageSize { get; }

        public GetAllBooksQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }

}
