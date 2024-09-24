using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.BooksUseCases
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
    }
}
