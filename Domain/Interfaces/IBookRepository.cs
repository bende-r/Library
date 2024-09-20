﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int id);

        Task<Book> GetByISBNAsync(string isbn);

        Task AddAsync(Book book);

        Task UpdateAsync(Book book);

        Task DeleteAsync(int id);

        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
    }
}