using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetToDoItemByIdQuery : IQuery<ToDoItem>
    {
        public Guid Id { get; set; }
    }
}
