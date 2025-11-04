namespace Todo.Application.Handlers.CompleteTodoItem;

using MediatR;
using Domain.Aggregates.TodoItem;
using Domain.Aggregates.TodoItem.Events;

public record CompleteTodoItemCommand(string Id) : IRequest<TodoItemCompleted>;

public class CompleteTodoItemCommandHandler(
    ITodoItemAggregateRepository repository) : IRequestHandler<CompleteTodoItemCommand, TodoItemCompleted>
{
    public Task<TodoItemCompleted> Handle(CompleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Id))
        {
            throw new ArgumentException(null, nameof(request));
        }

        var aggregate = repository.GetById(request.Id);
        aggregate.Complete();
        
        repository.Update(aggregate);

        var todoItemCompleted = aggregate.UncommittedEvents.First(x => x.GetType() == typeof(TodoItemCompleted)) as TodoItemCompleted;
        return Task.FromResult(todoItemCompleted!);
    }
}
