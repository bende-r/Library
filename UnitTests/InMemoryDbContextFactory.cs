using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class InMemoryDbContextFactory
    {
        public ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // создаем InMemory БД
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}