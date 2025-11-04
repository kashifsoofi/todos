using Todo.Domain.Aggregates.TodoItem.Commands;
using Todo.Domain.Aggregates.TodoItem.Events;
using Todo.Domain.Interfaces;

namespace Todo.Domain.Aggregates.TodoItem;

public class TodoItemAggregate : IAggregate
{
    private readonly bool _isNew;

    public TodoItemAggregate()
    {
        Id = "";
        Name = "";
        IsComplete = false;
        _isNew = true;
    }

    public TodoItemAggregate(string id, string name, bool isComplete)
    {
        Id = id;
        Name = name;
        IsComplete = isComplete;
        _isNew = false;
    }
        
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool IsComplete { get; private set; }

    private readonly List<IAggregateEvent> _uncommittedEvents = [];
    public IReadOnlyCollection<IAggregateEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    public void Create(CreateTodoItem command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_isNew)
        {
            throw new Exception("TodoItem already exists.");
        }
        
        Id = Guid.NewGuid().ToString("N")[..5];
        Name = command.Name;
        IsComplete = false;
        
        _uncommittedEvents.Add(new TodoItemCreated(Id, Name,  IsComplete));
    }

    public void Complete()
    {
        if (_isNew)
        {
            throw new Exception("TodoItem does not exist.");
        }
        
        IsComplete = true;
        
        _uncommittedEvents.Add(new TodoItemCompleted(Id,  Name));
    }

    public void Remove()
    {
        if (_isNew)
        {
            throw new Exception("TodoItem does not exist.");
        }
        
        _uncommittedEvents.Add(new TodoItemRemoved(Id, Name));
    }
}
