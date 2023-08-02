using Moq;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.QueryHandlers
{
    public class GetToDoItemsQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ReturnsAllToDoItems()
        {
            // Arrange
            var toDoItems = new List<ToDoItem>
        {
            new ToDoItem {Title = "Task 1", IsCompleted = false, IsDeleted = false },
            new ToDoItem {Title = "Task 2", IsCompleted = false, IsDeleted = false },
            new ToDoItem {Title = "Task 3", IsCompleted = false, IsDeleted = false }
        };

            var mockRepository = new Mock<IToDoItemRepository>();
            mockRepository.Setup(repo => repo.GetToDoItemsAsync()).ReturnsAsync(toDoItems);

            var handler = new GetToDoItemsQueryHandler(mockRepository.Object);

            var query = new GetToDoItemsQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(toDoItems.Count, result.Count);
            Assert.True(result.All(t => !t.IsDeleted));
        }
    }
}
