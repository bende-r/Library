using Domain.Entities;
using Domain.Models.Entities;

namespace Application.UseCases.BooksUseCases.GetPagedBooks;

public class GetPagedBooksResponse
{
    public PagedList<Book> books { get; set; }
}