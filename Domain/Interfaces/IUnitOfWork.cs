﻿namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }

        Task<int> SaveChangesAsync();
    }
}