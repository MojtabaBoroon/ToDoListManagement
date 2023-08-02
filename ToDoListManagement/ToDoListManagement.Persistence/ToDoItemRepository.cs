using Microsoft.EntityFrameworkCore;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;
using ToDoListManagement.Persistence.DataAccessContext;

namespace ToDoListManagement.Persistence
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoItemRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(ToDoItem toDoItem)
        {
            toDoItem.CreatedAt = DateTime.UtcNow;
            _dbContext.ToDoItems.Add(toDoItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ToDoItem>> GetToDoItemsAsync()
        {
            return await _dbContext.ToDoItems.Where(t => !t.IsDeleted).ToListAsync();
        }

        public async Task<List<ToDoItem>> GetCompletedToDoItemsAsync()
        {
            return await _dbContext.ToDoItems.Where(t => t.IsCompleted && !t.IsDeleted).ToListAsync();
        }

        public async Task<List<ToDoItem>> GetDeletedToDoItemsAsync()
        {
            return await _dbContext.ToDoItems.Where(t => t.IsDeleted).ToListAsync();
        }

        public async Task<ToDoItem> GetToDoItemByIdAsync(Guid id)
        {
            return await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }


        public async Task UpdateAsync(Guid id, ToDoItemDto toDoItem)
        {
            var existingTodo = await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (existingTodo != null)
            {
                if (toDoItem.Title != null)
                {
                    existingTodo.Title = toDoItem.Title;
                }

                if (toDoItem.Note != null)
                {
                    existingTodo.Note = toDoItem.Note;
                }

                if (toDoItem.IsCompleted.Value)
                {
                    existingTodo.IsCompleted = toDoItem.IsCompleted.Value;
                }
                existingTodo.LastModified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"ToDo item with ID {id} not found.");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            if (todo != null)
            {
                todo.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"ToDo item with ID {id} not found.");
            }
        }
    }
}
