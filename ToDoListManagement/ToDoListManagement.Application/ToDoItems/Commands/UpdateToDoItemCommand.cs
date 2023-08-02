using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.Dtos.ToDoItemDtos;

namespace ToDoListManagement.Application.ToDoItems.Commands
{
    public class UpdateToDoItemCommand : ICommand
    {
        public Guid Id { get; set; }
        public ToDoItemDto ToDoItemDto { get; set; }
    }
}
