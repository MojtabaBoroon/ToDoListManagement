using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetToDoItemsQueryHandler : IQueryHandler<GetToDoItemsQuery, List<ToDoItem>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public async Task<List<ToDoItem>> HandleAsync(GetToDoItemsQuery command)
        {
            return await _toDoItemRepository.GetToDoItemsAsync();
        }
    }
}
