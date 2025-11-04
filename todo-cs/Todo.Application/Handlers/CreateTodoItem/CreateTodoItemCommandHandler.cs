namespace Todo.Application.Handlers.CreateTodoItem;

using MediatR;
using Domain.Aggregates.TodoItem;
using Domain.Aggregates.TodoItem.Events;
using CreateTodoItem = Domain.Aggregates.TodoItem.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string Name) : IRequest<TodoItemCreated>;

public class CreateTodoItemCommandHandler(
    ITodoItemAggregateRepository repository) : IRequestHandler<CreateTodoItemCommand, TodoItemCreated>
{
    public Task<TodoItemCreated> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Name))
        {
            throw new ArgumentException(null, nameof(request));
        }

        var aggregate = new TodoItemAggregate();
        aggregate.Create(new CreateTodoItem(request.Name));
        
        repository.Create(aggregate);

        var todoItemCreated = aggregate.UncommittedEvents.First(x => x.GetType() == typeof(TodoItemCreated)) as TodoItemCreated;
        return Task.FromResult(todoItemCreated!);
    }
}
