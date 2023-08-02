namespace ToDoListManagement.Presentation.Contracts.ToDoItems.Responses;

public record ToDoItemInsertResponse(Guid Id, string Title, string Note, bool IsCompleted);
