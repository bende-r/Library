using Domain.Interfaces;

using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public IUserBookRepository UserBooks { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
            UserBooks = new UserBookRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}