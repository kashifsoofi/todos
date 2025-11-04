namespace Todo.Application.Handlers.GetAllTodoItems;

using MediatR;
using Domain.Aggregates.TodoItem;
using Domain.Aggregates.TodoItem.Events;

public record GetAllTodoItemsQuery : IRequest<GetAllTodoItemsResponse>;

public class GetAllTodoItemsQueryHandler(
    ITodoItemAggregateRepository repository) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResponse>
{
    public Task<GetAllTodoItemsResponse> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var aggregates = repository.GetAll();
        var todoItems = aggregates.Select(x => new TodoItem(x.Id,
            x.Name,
            x.IsComplete))
            .ToList();
        return Task.FromResult(new GetAllTodoItemsResponse(todoItems));
    }
}

public record GetAllTodoItemsResponse(List<TodoItem> TodoItems);
public record TodoItem(
    string Id,
    string Name,
    bool IsComplete);