using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Persistence.Abstractions;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class UpdateToDoItemCommandHandler : ICommandHandler<UpdateToDoItemCommand>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public UpdateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task HandleAsync(UpdateToDoItemCommand command)
        {
            await _toDoItemRepository.UpdateAsync(command.Id, command.ToDoItemDto);
        }
    }
}
