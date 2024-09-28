namespace Domain.Entities;

public class Author
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }

    // Список книг, написанных этим автором
    public ICollection<Book> Books { get; set; }
}