using Moq;
using ToDoListManagement.Application.ToDoItems.Commands;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.CommandHandlers
{
    public class DeleteToDoItemCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ValidId_CallsDeleteAsync()
        {
            // Arrange
            var mockRepository = new Mock<IToDoItemRepository>();
            var handler = new DeleteToDoItemCommandHandler(mockRepository.Object);

            var toDoItemId = Guid.NewGuid();
            var command = new DeleteToDoItemCommand
            {
                Id = toDoItemId
            };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.DeleteAsync(toDoItemId), Times.Once);
        }
    }
}
