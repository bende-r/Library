using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.UseCases.AuthorsUseCases.AddAuthor;
using Application.UseCases.AuthorsUseCases;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.UseCasesTest
{
    public class GetAuthorByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAuthorByIdQueryHandler _handler;

        public GetAuthorByIdQueryHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAuthorByIdQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                Id = authorId,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            };

            var authorResponse = new AddAuthorResponse
            {
                Id = author.Id,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            };

            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map<AddAuthorResponse>(author)).Returns(authorResponse);

            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync((Author)null);

            var query = new GetAuthorByIdQuery(authorId);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
            Assert.Equal("Author not found", exception.Message);
        }
    }
}
