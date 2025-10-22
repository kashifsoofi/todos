namespace Todo.Domain;

public record TodoItem(
    string Id,
    string Name,
    bool IsComplete);