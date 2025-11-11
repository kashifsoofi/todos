namespace Todo.BlazorWasm.Models;

public record TodoItem(
    string Id,
    string Name,
    bool IsComplete)
{
    public TodoItem() : this(string.Empty, string.Empty, false)
    { }
    
    public string Id { get; } = Id;
    public string Name { get; } = Name;
    public bool IsComplete { get; set; }  = IsComplete;
}
