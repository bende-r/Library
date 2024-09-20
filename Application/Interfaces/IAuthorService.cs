using Application.DTOs;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        /// <summary>
        /// Получение списка всех авторов.
        /// </summary>
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

        /// <summary>
        /// Получение автора по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        Task<AuthorDto> GetAuthorByIdAsync(int id);

        /// <summary>
        /// Добавление нового автора.
        /// </summary>
        /// <param name="authorDto">Данные нового автора</param>
        Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto);

        /// <summary>
        /// Обновление информации об авторе.
        /// </summary>
        /// <param name="authorDto">Обновленные данные автора</param>
        Task UpdateAuthorAsync(AuthorDto authorDto);

        /// <summary>
        /// Удаление автора по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        Task DeleteAuthorAsync(int id);
    }
}
