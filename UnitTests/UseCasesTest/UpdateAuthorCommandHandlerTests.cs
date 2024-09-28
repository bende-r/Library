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
    public class UpdateAuthorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateAuthorCommandHandler _handler;

        public UpdateAuthorCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateAuthorCommandHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                Id = authorId,
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "OldCountry"
            };

            var updatedAuthorCommand = new UpdateAuthorCommand
            {
                Id = authorId,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "NewCountry"
            };

            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map(updatedAuthorCommand, author));
            _mockMapper.Setup(m => m.Map<UpdateAuthorResponse>(author)).Returns(new UpdateAuthorResponse
            {
                Id = author.Id,
                FirstName = updatedAuthorCommand.FirstName,
                LastName = updatedAuthorCommand.LastName,
                DateOfBirth = updatedAuthorCommand.DateOfBirth,
                Country = updatedAuthorCommand.Country
            });

            // Act
            var result = await _handler.Handle(updatedAuthorCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedAuthorCommand.FirstName, result.FirstName);
            Assert.Equal(updatedAuthorCommand.LastName, result.LastName);
            Assert.Equal(updatedAuthorCommand.Country, result.Country);

            // Проверяем, что методы UpdateAsync и CompleteAsync были вызваны
            _mockUnitOfWork.Verify(uow => uow.Authors.UpdateAsync(author), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var updateCommand = new UpdateAuthorCommand
            {
                Id = authorId,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "NewCountry"
            };

            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync((Author)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(updateCommand, CancellationToken.None));
            Assert.Equal("Author not found", exception.Message);
        }
    }
}
