using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books) // Включаем связанные книги
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
        }
    }
}