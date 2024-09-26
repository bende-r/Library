using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        Task<IEnumerable<Book>> GetBooksTakenByUserAsync(string userId);
        Task<UserBook?> FindAsync(Expression<Func<UserBook, bool>> predicate, CancellationToken cancellationToken);
    }

}
