using Application.DTOs;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Получение списка всех книг.
        /// </summary>
        Task<IEnumerable<BookDto>> GetAllBooksAsync();

        Task<PagedResult<BookDto>> GetAllBooksAsync(int page, int pageSize);


        /// <summary>
        /// Получение книги по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        Task<BookDto> GetBookByIdAsync(int id);

        /// <summary>
        /// Получение книги по ISBN.
        /// </summary>
        /// <param name="isbn">ISBN книги</param>
        Task<BookDto> GetBookByISBNAsync(string isbn);

        /// <summary>
        /// Добавление новой книги.
        /// </summary>
        /// <param name="bookDto">Данные новой книги</param>
        Task<BookDto> AddBookAsync(BookDto bookDto);

        /// <summary>
        /// Обновление информации о книге.
        /// </summary>
        /// <param name="bookDto">Обновленные данные книги</param>
        Task UpdateBookAsync(BookDto bookDto);

        /// <summary>
        /// Удаление книги по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        Task DeleteBookAsync(int id);

        /// <summary>
        /// Получение списка книг по идентификатору автора.
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(int authorId);
    }
}
