using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BorrowedBook
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        // Ссылка на автора книги
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public bool IsBorrowed { get; set; }

        // Путь к изображению книги
        public string? ImageUrl { get; set; }

        // Дата, когда книгу взяли
        public DateTime? BorrowedDate { get; set; }

        // Дата, когда книгу нужно вернуть
        public DateTime? ReturnDate { get; set; }
    }
}
