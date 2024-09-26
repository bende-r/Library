using Domain.Entities;
using Domain.Models.Entities;

using Microsoft.Extensions.Logging;

namespace Domain.Interfaces
{

    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetByISBNAsync(string isbn);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
        IQueryable<Book> FindAll();

        Task<PagedList<Book>> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }


}