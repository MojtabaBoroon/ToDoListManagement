using ToDoListManagement.Application.Abstraction;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Application.ToDoItems.Queries
{
    public class GetCompletedToDoItemsQuery : IQuery<List<ToDoItem>>
    {
    }
}
