using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetToDoItemByIdQueryHandler : IQueryHandler<GetToDoItemByIdQuery, ToDoItem>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetToDoItemByIdQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public async Task<ToDoItem> HandleAsync(GetToDoItemByIdQuery command)
        {
            return await _toDoItemRepository.GetToDoItemByIdAsync(command.Id);
        }
    }
}
