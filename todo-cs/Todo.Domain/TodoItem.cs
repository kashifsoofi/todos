namespace Todo.Domain;

public record TodoItem(
    string Id,
    string Name,
    bool IsComplete)
{
    public bool IsComplete { get; set; } = IsComplete;
}