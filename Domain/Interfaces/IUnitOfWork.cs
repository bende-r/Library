namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IUserBookRepository UserBooks { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }

}