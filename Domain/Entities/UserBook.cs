namespace Domain.Entities;

public class UserBook
{
    public Guid Id { get; set; }

    // Ссылка на книгу
    public Guid BookId { get; set; }

    public Book Book { get; set; }

    // Ссылка на пользователя
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    // Дата, когда книгу взяли
    public DateTime? BorrowedDate { get; set; }

    // Дата, когда книгу нужно вернуть
    public DateTime? ReturnDate { get; set; }

    // Флаг, указывающий, вернули ли книгу
    public bool IsReturned { get; set; }
}