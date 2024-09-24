using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
  

    public class AddBookCommand : IRequest<AddBookResponse>
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }

}
