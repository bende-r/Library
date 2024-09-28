using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.UseCases.AuthorsUseCases;
using Application.UseCases.AuthorsUseCases.AddAuthor;
using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Moq;

namespace Tests.UseCasesTest
{
    public class AddAuthorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly AddAuthorCommandHandler _handler;

        public AddAuthorCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Настройка маппинга
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAuthorCommand, Author>();
                cfg.CreateMap<Author, AddAuthorResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new AddAuthorCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldAddAuthorSuccessfully()
        {
            // Arrange
            var command = new AddAuthorCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            };

            _mockUnitOfWork.Setup(uow => uow.Authors.AddAsync(It.IsAny<Author>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.FromResult(0));


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);

            // Проверка вызова методов репозитория
            _mockUnitOfWork.Verify(uow => uow.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
