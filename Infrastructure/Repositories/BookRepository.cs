using Domain.Entities;
using Domain.Interfaces;
using Domain.Models.Entities;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Book> GetByISBNAsync(string isbn)
        {
            return await _dbSet.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _dbSet.Where(b => b.AuthorId == authorId).ToListAsync();
        }

        public IQueryable<Book> FindAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<PagedList<Book>> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return PagedList<Book>.ToPagedList(FindAll().OrderBy(e => e.Title),
                pageNumber,
                pageSize);
        }
    }
}