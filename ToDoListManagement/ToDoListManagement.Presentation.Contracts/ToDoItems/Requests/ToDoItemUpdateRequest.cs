namespace ToDoListManagement.Presentation.Contracts.ToDoItems.Requests;

public record ToDoItemUpdateRequest(Guid Id, string Title, string Note, bool IsCompleted);
