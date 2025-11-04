namespace Todo.Application.Handlers.RemoveTodoItem;

using MediatR;
using Domain.Aggregates.TodoItem;
using Domain.Aggregates.TodoItem.Events;

public record RemoveTodoItemCommand(string Id) : IRequest<TodoItemRemoved>;

public class CompleteTodoItemCommandHandler(
    ITodoItemAggregateRepository repository) : IRequestHandler<RemoveTodoItemCommand, TodoItemRemoved>
{
    public Task<TodoItemRemoved> Handle(RemoveTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Id))
        {
            throw new ArgumentException(null, nameof(request));
        }

        var aggregate = repository.GetById(request.Id);
        aggregate.Remove();
        
        repository.Delete(aggregate);

        var todoItemRemoved = aggregate.UncommittedEvents.First(x => x.GetType() == typeof(TodoItemRemoved)) as TodoItemRemoved;
        return Task.FromResult(todoItemRemoved!);
    }
}
