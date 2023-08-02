using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class InsertToDoItemCommandHandler : ICommandHandler<InsertToDoItemCommand>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public InsertToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task HandleAsync(InsertToDoItemCommand command)
        {
            await _toDoItemRepository.InsertAsync(command.ToDoItem);
        }
    }
}
