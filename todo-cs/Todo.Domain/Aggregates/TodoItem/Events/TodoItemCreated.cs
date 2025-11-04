using Todo.Domain.Interfaces;

namespace Todo.Domain.Aggregates.TodoItem.Events;

public record TodoItemCreated(
    string Id,
    string Name,
    bool IsComplete) : AggregateEvent;