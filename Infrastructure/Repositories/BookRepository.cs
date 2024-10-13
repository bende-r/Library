using Domain.Entities;
using Domain.Interfaces;
using Domain.Models.Entities;

using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<Book> GetByIdAsync(Guid id)
        {
            return await _dbSet
       .Include(book => book.Author)
       .FirstOrDefaultAsync(book => book.Id == id);
        }

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
            var booksQuery = FindAll()
                .Include(b => b.Author)  // Включаем автора для каждой книги
                .OrderBy(e => e.Title);  // Сортируем книги по заголовку

            return PagedList<Book>.ToPagedList(booksQuery, pageNumber, pageSize);
        }
    }
}