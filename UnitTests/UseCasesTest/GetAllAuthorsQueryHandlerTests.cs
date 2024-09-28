using Application.UseCases.AuthorsUseCases;
using Application.UseCases.AuthorsUseCases.AddAuthor;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Moq;

namespace Tests.UseCasesTest
{
    public class GetAllAuthorsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllAuthorsQueryHandler _handler;

        public GetAllAuthorsQueryHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllAuthorsQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfAuthors_WhenAuthorsExist()
        {
            // Arrange
            var authors = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1970, 1, 1), Country = "USA" },
            new Author { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), Country = "UK" }
        };

            var authorResponses = new List<AuthorResponse>
        {
            new AuthorResponse { Id = authors[0].Id, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1970, 1, 1), Country = "USA" },
            new AuthorResponse { Id = authors[1].Id, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), Country = "UK" }
        };

            _mockUnitOfWork.Setup(uow => uow.Authors.GetAllAsync()).ReturnsAsync(authors); // Мокаем получение авторов
            _mockMapper.Setup(m => m.Map<IEnumerable<AuthorResponse>>(authors)).Returns(authorResponses); // Мокаем маппинг

            var query = new GetAllAuthorsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("John", result.First().FirstName);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAuthorsExist()
        {
            // Arrange
            var emptyAuthorList = new List<Author>();
            var emptyResponseList = new List<AuthorResponse>();

            _mockUnitOfWork.Setup(uow => uow.Authors.GetAllAsync()).ReturnsAsync(emptyAuthorList);
            _mockMapper.Setup(m => m.Map<IEnumerable<AuthorResponse>>(emptyAuthorList)).Returns(emptyResponseList);

            var query = new GetAllAuthorsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);  // Проверяем, что список пуст
        }
    }
}