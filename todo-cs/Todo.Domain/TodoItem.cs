namespace Todo.Domain;

public record TodoItem(
    string Id,
    string Name,
    bool IsComplete)
{
    public TodoItem()
        : this(Guid.NewGuid().ToString("N")[..5], string.Empty, false)
    {
    }

    public bool IsComplete { get; set; } = IsComplete;
}