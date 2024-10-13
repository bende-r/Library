using Domain.Entities;

namespace Application.UseCases.EventUseCases.GetAllUserEvents;

public class GetAllUserBooksResponse
{
    public IEnumerable<BorrowedBook> books { get; set; }
}