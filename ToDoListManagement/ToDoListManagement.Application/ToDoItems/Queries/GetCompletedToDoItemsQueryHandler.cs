using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetCompletedToDoItemsQueryHandler : IQueryHandler<GetCompletedToDoItemsQuery, List<ToDoItem>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetCompletedToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public async Task<List<ToDoItem>> HandleAsync(GetCompletedToDoItemsQuery command)
        {
            return await _toDoItemRepository.GetCompletedToDoItemsAsync();
        }
    }
}
