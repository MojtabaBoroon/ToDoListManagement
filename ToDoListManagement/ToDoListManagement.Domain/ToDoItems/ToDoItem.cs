using ToDoListManagement.Domain.Common;

namespace ToDoListManagement.Domain.ToDoItems
{
    public class ToDoItem : BaseEntity
    {
        public string? Title { get; set; }
        public string? Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public ToDoItem()
        {
            IsCompleted = false;
        }
    }
}
