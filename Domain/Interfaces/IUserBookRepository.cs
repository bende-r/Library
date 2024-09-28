using System.Linq.Expressions;

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        Task<IEnumerable<Book>> GetBooksTakenByUserAsync(string userId);

        Task<UserBook?> FindAsync(Expression<Func<UserBook, bool>> predicate, CancellationToken cancellationToken);
    }
}