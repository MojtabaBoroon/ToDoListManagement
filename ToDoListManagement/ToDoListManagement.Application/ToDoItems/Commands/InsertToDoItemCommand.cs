using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class InsertToDoItemCommand : ICommand
    {
        public ToDoItem ToDoItem { get; set; }
    }
}
