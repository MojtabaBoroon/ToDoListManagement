namespace ToDoListManagement.Presentation.Contracts.ToDoItems.Requests;

public record ToDoItemInsertRequest(string Title, string Note, bool IsCompleted);
