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

        public async Task<IEnumerable<BorrowedBook>> GetBooksTakenByUserAsync(string userId)
        {
            var userEv = await _dbSet
                .Where(eu => eu.UserId == userId)
                .Include(eu => eu.Book)
                .Include(eu => eu.Book.Author)
                .Select(ub => new BorrowedBook
                {
                    Id = ub.Book.Id,
                    ISBN = ub.Book.ISBN,
                    Title = ub.Book.Title,
                    Genre = ub.Book.Genre,
                    Description = ub.Book.Description,
                    Author = ub.Book.Author,
                    AuthorId = ub.Book.AuthorId,
                    IsBorrowed = ub.Book.IsBorrowed,

                    ImageUrl = ub.Book.ImageUrl,

                    BorrowedDate = ub.BorrowedDate,
                    ReturnDate = ub.ReturnDate,
                })
        .ToListAsync();

            return userEv;
        }

        public async Task<UserBook?> FindAsync(Expression<Func<UserBook, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}