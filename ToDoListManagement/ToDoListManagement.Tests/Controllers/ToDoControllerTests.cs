using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.DataAccessContext;
using ToDoListManagement.Presentation.Api.Controllers;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Requests;
using ToDoListManagement.Presentation.Contracts.ToDoItems.Responses;

namespace ToDoListManagement.Tests.Controllers
{
    public class ToDoControllerTests : IClassFixture<TestStartup<Program>>
    {

        private readonly TestStartup<Program> _factory;
        private readonly HttpClient _client;

        private Guid _id = Guid.NewGuid();


        public ToDoControllerTests(TestStartup<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
        private void SeedDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                dbContext.Database.EnsureDeleted();

                dbContext.Database.EnsureCreated();

                var toDoItems = new List<ToDoItem>
            {
                new ToDoItem {Title = "Task 1", Note = "Note1", IsDeleted = false, IsCompleted = true},
                new ToDoItem {Id = _id, Title = "Task 2", Note = "Note2", IsDeleted = false, IsCompleted = false},
                new ToDoItem {Title = "Task 3", Note = "Note3", IsDeleted = true, IsCompleted = true},
                new ToDoItem {Title = "Task 4", Note = "Note4", IsDeleted = true, IsCompleted = true},
                new ToDoItem {Title = "Task 5", Note = "Note5", IsDeleted = true, IsCompleted = true}
            };
                dbContext.ToDoItems.AddRange(toDoItems);
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetToDoItems_ReturnsToDoItems()
        {           
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.GetAsync("/api/ToDo");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(responseContent);
 
            // Assert
            Assert.NotNull(toDoItems);
            Assert.Equal(2, toDoItems.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItems_ReturnsCompletedToDoItems()
        {
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.GetAsync("/api/ToDo/Completed");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(responseContent);

            // Assert
            Assert.NotNull(toDoItems);
            Assert.Equal(1, toDoItems.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItems_ReturnsDeletedToDoItems()
        {
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.GetAsync("/api/ToDo/Deleted");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(responseContent);

            // Assert
            Assert.NotNull(toDoItems);
            Assert.Equal(3, toDoItems.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItemById_IdIsValid_ReturnsToDoItem()
        {
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.GetAsync($"/api/ToDo/{_id}");
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var todoItem = JsonConvert.DeserializeObject<ToDoItem>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(todoItem);
            Assert.Equal(_id, todoItem.Id);
            Assert.Equal("Task 2", todoItem.Title);
            Assert.False(todoItem.IsDeleted);
            Assert.False(todoItem.IsCompleted);
        }

        [Fact]
        public async Task GetToDoItemById_IdIsNotValid_ReturnsNotFound()
        {
            // Arrange
            SeedDatabase();
            var id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/ToDo/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Theory]
        [InlineData(1)]
        [InlineData("a")]
        public async Task GetToDoItemById_IdIsNotValid_ReturnsBadRequest(object id)
        {
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.GetAsync($"/api/ToDo/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetEnrichedTransactions_ReturnsCreatedAtAction()
        {
            // Arrange
            SeedDatabase();

            var newItemRequest = new ToDoItemInsertRequest("New Task", "Note", false);
            var content = new StringContent(JsonConvert.SerializeObject(newItemRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/ToDo", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var toDoItemInsertResponse = JsonConvert.DeserializeObject<ToDoItemInsertResponse>(responseContent);
            Assert.NotNull(toDoItemInsertResponse);
        }

        [Fact]
        public async Task Put_ReturnsNoContentResult()
        {
            // Arrange
            SeedDatabase();
            var request = new ToDoItemUpdateRequest(_id, "New Title", "New Note", false);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/ToDo", content);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Put_IdIsInvalid_ReturnsNotFound()
        {
            // Arrange
            SeedDatabase();
            var nonExistentId = Guid.NewGuid();
            var request = new ToDoItemUpdateRequest(nonExistentId, "New Title", "New Note", false);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/ToDo", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            SeedDatabase();

            // Act
            var response = await _client.DeleteAsync($"/api/ToDo/{_id}");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_IdIsInvalid_ReturnsNoContentResult()
        {
            // Arrange
            SeedDatabase();
            var nonExistentId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/ToDo/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

