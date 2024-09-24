using Domain.Entities;

namespace Domain.Interfaces
{
   
        public interface IBookRepository : IRepository<Book>
        {
            Task<Book> GetByISBNAsync(string isbn);
            Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
        }

   
}