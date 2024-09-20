using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetByISBNAsync(string isbn)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}