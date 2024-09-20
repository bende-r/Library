using Domain.Interfaces;

using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
        }

        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}