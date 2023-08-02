using Moq;
using ToDoListManagement.Application.ToDoItems.Commands;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.CommandHandlers
{
    public class UpdateToDoItemCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ValidIdAndToDoItemDto_CallsUpdateAsync()
        {
            // Arrange
            var mockRepository = new Mock<IToDoItemRepository>();
            var handler = new UpdateToDoItemCommandHandler(mockRepository.Object);

            var toDoItemId = Guid.NewGuid(); 
            var toDoItemDto = new ToDoItemDto
            {
                Note = "Note"
            };

            var command = new UpdateToDoItemCommand
            {
                Id = toDoItemId,
                ToDoItemDto = toDoItemDto
            };

            // Act
            await handler.HandleAsync(command);

            // Assert
            mockRepository.Verify(repo => repo.UpdateAsync(toDoItemId, toDoItemDto), Times.Once);
        }
    }
}
