using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId)
        {
            return await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        }
    }
}