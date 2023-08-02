using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class DeleteToDoItemCommandHandler : ICommandHandler<DeleteToDoItemCommand>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task HandleAsync(DeleteToDoItemCommand command)
        {
            await _toDoItemRepository.DeleteAsync(command.Id);
        }
    }
}
