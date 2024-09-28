using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class UpdateBookCommand : IRequest<UpdateBookResponse>
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
