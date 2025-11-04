using Todo.Domain.Interfaces;

namespace Todo.Domain.Aggregates.TodoItem.Events;

public record TodoItemRemoved(string Id, string Name) : AggregateEvent;