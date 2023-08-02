using Moq;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.QueryHandlers
{
    public class GetCompletedToDoItemsQueryHandlerTests
    {

        [Fact]
        public async Task HandleAsync_ReturnsCompletedToDoItems()
        {
            // Arrange
            var completedToDoItems = new List<ToDoItem>
        {
            new ToDoItem {Title = "Task 1", IsCompleted = true },
            new ToDoItem {Title = "Task 2", IsCompleted = true },
            new ToDoItem {Title = "Task 3", IsCompleted = true }
        };

            var mockRepository = new Mock<IToDoItemRepository>();
            mockRepository
                .Setup(repo => repo.GetCompletedToDoItemsAsync())
                .ReturnsAsync(completedToDoItems);

            var queryHandler = new GetCompletedToDoItemsQueryHandler(mockRepository.Object);
            var query = new GetCompletedToDoItemsQuery();

            // Act
            var result = await queryHandler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(completedToDoItems.Count, result.Count);

            for (int i = 0; i < completedToDoItems.Count; i++)
            {
                Assert.Equal(completedToDoItems[i].Id, result[i].Id);
                Assert.Equal(completedToDoItems[i].Title, result[i].Title);
                Assert.True(result[i].IsCompleted);
            }
        }
    }
}
