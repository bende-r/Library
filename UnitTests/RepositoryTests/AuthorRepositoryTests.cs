using Domain.Entities;

using Infrastructure.Repositories;

namespace Tests.RepositoryTests
{
    public class AuthorRepositoryTests
    {
        private readonly AuthorRepository _repository;
        private readonly InMemoryDbContextFactory _dbContextFactory;

        public AuthorRepositoryTests()
        {
            _dbContextFactory = new InMemoryDbContextFactory();
            var context = _dbContextFactory.CreateContext();
            _repository = new AuthorRepository(context);
        }

        [Fact]
        public async Task AddAuthor_ShouldAddAuthorToDatabase()
        {
            // Arrange
            var context = _dbContextFactory.CreateContext(); // Создаем один контекст для добавления и проверки данных
            var repository = new AuthorRepository(context);  // Создаем репозиторий, используя этот же контекст

            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            };

            // Act
            await repository.AddAsync(author);  // Добавляем автора через репозиторий
            await context.SaveChangesAsync();   // Сохраняем изменения в базе данных

            // Проверяем, что автор добавлен в базу данных
            var result = await context.Authors.FindAsync(author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthor()
        {
            // Arrange
            var context = _dbContextFactory.CreateContext(); // Создаем один контекст
            var repository = new AuthorRepository(context);  // Создаем репозиторий с этим контекстом

            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            };

            await context.Authors.AddAsync(author);  // Добавляем автора
            await context.SaveChangesAsync();        // Сохраняем изменения в базе данных

            // Проверяем, что автор добавлен
            var addedAuthor = await context.Authors.FindAsync(author.Id);
            Assert.NotNull(addedAuthor);  // Убедитесь, что автор был добавлен

            // Act
            var result = await repository.GetByIdAsync(author.Id); // Используем тот же контекст для поиска

            // Assert
            Assert.NotNull(result);  // Убедитесь, что автор найден
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }
    }
}