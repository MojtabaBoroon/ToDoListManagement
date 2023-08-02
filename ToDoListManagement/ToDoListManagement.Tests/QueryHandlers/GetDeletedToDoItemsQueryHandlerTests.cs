using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListManagement.Application.ToDoItems.Queries;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Tests.QueryHandlers
{
    public class GetDeletedToDoItemsQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ReturnsDeletedToDoItems()
        {
            // Arrange
            var deletedToDoItems = new List<ToDoItem>
        {
            new ToDoItem {Title = "Task 1", IsDeleted = true },
            new ToDoItem {Title = "Task 2", IsDeleted = true },
            new ToDoItem {Title = "Task 3", IsDeleted = true }
        };

            var mockRepository = new Mock<IToDoItemRepository>();
            mockRepository
                .Setup(repo => repo.GetDeletedToDoItemsAsync())
                .ReturnsAsync(deletedToDoItems);

            var queryHandler = new GetDeletedToDoItemsQueryHandler(mockRepository.Object);
            var query = new GetDeletedToDoItemsQuery();

            // Act
            var result = await queryHandler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(deletedToDoItems.Count, result.Count);

            for (int i = 0; i < deletedToDoItems.Count; i++)
            {
                Assert.Equal(deletedToDoItems[i].Id, result[i].Id);
                Assert.Equal(deletedToDoItems[i].Title, result[i].Title);
                Assert.True(result[i].IsDeleted);
            }
        }
    }
}
