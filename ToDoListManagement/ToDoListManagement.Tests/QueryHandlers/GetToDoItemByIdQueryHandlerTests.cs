using Moq;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.QueryHandlers
{
    public class GetToDoItemByIdQueryHandlerTests
    {
        Guid _todoItemId = Guid.NewGuid();

        [Fact]
        public async Task HandleAsync_ValidId_ReturnsToDoItem()
        {
            // Arrange
            var mockRepository = new Mock<IToDoItemRepository>();
            mockRepository.Setup(repo => repo.GetToDoItemByIdAsync(_todoItemId)).ReturnsAsync(new ToDoItem
            {
                Id = _todoItemId,
                Title = "Test ToDo Item",
            });

            var handler = new GetToDoItemByIdQueryHandler(mockRepository.Object);

            var query = new GetToDoItemByIdQuery
            {
                Id = _todoItemId
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_todoItemId, result.Id);
        }

        [Fact]
        public async Task HandleAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var mockRepository = new Mock<IToDoItemRepository>();
            mockRepository.Setup(repo => repo.GetToDoItemByIdAsync(_todoItemId)).ReturnsAsync((ToDoItem)null);

            var handler = new GetToDoItemByIdQueryHandler(mockRepository.Object);

            var query = new GetToDoItemByIdQuery
            {
                Id = _todoItemId
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.Null(result);
        }
    }
}
