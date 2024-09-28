using Domain.Entities;

namespace Application.UseCases.EventUseCases.GetAllUserEvents;

public class GetAllUserBooksResponse
{
    public IEnumerable<Book> books { get; set; }
}