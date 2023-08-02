using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetToDoItemsQuery : IQuery<List<ToDoItem>>
    {
    }
}
