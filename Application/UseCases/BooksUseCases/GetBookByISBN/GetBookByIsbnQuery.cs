using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIsbnQuery : IRequest<BookResponse>
    {
        public string ISBN { get; set; }

        public GetBookByIsbnQuery(string isbn)
        {
            ISBN = isbn;
        }
    }
}
