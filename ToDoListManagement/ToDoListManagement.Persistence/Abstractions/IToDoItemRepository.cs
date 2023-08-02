using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Persistence.Abstractions
{
    public interface IToDoItemRepository
    {
        Task InsertAsync(ToDoItem toDoItem);
        Task<List<ToDoItem>> GetToDoItemsAsync();
        Task<List<ToDoItem>> GetCompletedToDoItemsAsync();
        Task<List<ToDoItem>> GetDeletedToDoItemsAsync();
        Task<ToDoItem> GetToDoItemByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ToDoItemDto toDoItem);
        Task DeleteAsync(Guid id);
    }
}
