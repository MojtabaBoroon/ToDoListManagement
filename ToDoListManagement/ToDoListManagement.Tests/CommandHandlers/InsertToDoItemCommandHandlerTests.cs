using Moq;
using ToDoListManagement.Application.ToDoItems.Commands;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.CommandHandlers
{
    public class InsertToDoItemCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ValidToDoItem_CallsInsertAsync()
        {
            // Arrange
            var mockRepository = new Mock<IToDoItemRepository>();
            var handler = new InsertToDoItemCommandHandler(mockRepository.Object);

            var todoItem = new ToDoItem();

            var command = new InsertToDoItemCommand();
            command.ToDoItem = todoItem;

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.InsertAsync(todoItem), Times.Once);
        }
    }
}
