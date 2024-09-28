namespace Domain.Entities;

public class UserBook
{
    public Guid Id { get; set; }

    // ������ �� �����
    public Guid BookId { get; set; }

    public Book Book { get; set; }

    // ������ �� ������������
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    // ����, ����� ����� �����
    public DateTime? BorrowedDate { get; set; }

    // ����, ����� ����� ����� �������
    public DateTime? ReturnDate { get; set; }

    // ����, �����������, ������� �� �����
    public bool IsReturned { get; set; }
}