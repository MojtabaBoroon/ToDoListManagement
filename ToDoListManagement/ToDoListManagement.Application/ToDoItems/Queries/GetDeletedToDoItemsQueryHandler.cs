using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetDeletedToDoItemsQueryHandler : IQueryHandler<GetDeletedToDoItemsQuery, List<ToDoItem>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetDeletedToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public async Task<List<ToDoItem>> HandleAsync(GetDeletedToDoItemsQuery command)
        {
            return await _toDoItemRepository.GetDeletedToDoItemsAsync();
        }
    }
}
