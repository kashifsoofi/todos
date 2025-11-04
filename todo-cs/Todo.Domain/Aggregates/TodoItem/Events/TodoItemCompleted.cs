using Todo.Domain.Interfaces;

namespace Todo.Domain.Aggregates.TodoItem.Events;

public record TodoItemCompleted(string Id, string Name) : AggregateEvent;