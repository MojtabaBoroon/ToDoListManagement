namespace ToDoListManagement.Presentation.Contracts.ToDoItems.Responses;

public record ToDoItemGetResponse(Guid Id, string Title, string Note, bool IsCompleted, DateTime CreatedAt, DateTime LastModified);
