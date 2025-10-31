using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Aggregates.TodoItem.Commands;
using Todo.Domain.Interfaces;

namespace Todo.Domain.Aggregates.TodoItem;

public class TodoItemAggregate : IAggregate
{
    private readonly List<IAggregateEvent> _uncommittedEvents;

    public IReadOnlyCollection<IAggregateEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    public void Create(CreateTodoItem command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));
    }
}
