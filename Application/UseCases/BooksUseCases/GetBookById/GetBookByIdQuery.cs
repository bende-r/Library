using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class GetBookByIdQuery : IRequest<BookResponse>
    {
        public Guid Id { get; set; }

        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
