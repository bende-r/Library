using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId);
    }
}