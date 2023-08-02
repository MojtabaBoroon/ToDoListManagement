using ToDoListManagement.Application.Abstraction;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class DeleteToDoItemCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
