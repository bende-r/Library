using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<UserBook>> GetBooksTakenByUserAsync(string userId)
        {
            return await _dbSet.Where(ub => ub.UserId == userId).ToListAsync();
        }

        public async Task<UserBook?> FindAsync(Expression<Func<UserBook, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }

}
