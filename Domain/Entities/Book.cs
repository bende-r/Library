using System;

using Domain.Entities;

namespace Domain.Entities;
public class Book
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

    // Список книг, взятых пользователями
    public ICollection<UserBook> UserBooks { get; set; }
}
