using System.Linq.Expressions;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksTakenByUserAsync(string userId)
        {
            var userEv = await _dbSet
                .Where(eu => eu.UserId == userId)
                .Include(eu => eu.Book)
                .Select(eu => eu.Book
                )
                .ToListAsync();

            return userEv;
        }

        public async Task<UserBook?> FindAsync(Expression<Func<UserBook, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}