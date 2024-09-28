using Application.UseCases.AuthorsUseCases.AddAuthor;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

using Moq;

namespace Tests.UseCasesTest
{
    public class DeleteAuthorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteAuthorCommandHandler _handler;

        public DeleteAuthorCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteAuthorCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId, FirstName = "John", LastName = "Doe" };

            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId))
                .ReturnsAsync(author); // Возвращаем автора
            _mockUnitOfWork.Setup(uow => uow.Authors.DeleteAsync(authorId))
                .Returns(Task.CompletedTask); // Имитируем успешное удаление
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync())
                .ReturnsAsync(1); // Имитируем успешное завершение транзакции

            var command = new DeleteAuthorCommand(authorId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _mockUnitOfWork.Verify(uow => uow.Authors.DeleteAsync(authorId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.Authors.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null); // Возвращаем null, чтобы имитировать отсутствие автора

            var command = new DeleteAuthorCommand(authorId);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _mockUnitOfWork.Verify(uow => uow.Authors.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
        }
    }
}