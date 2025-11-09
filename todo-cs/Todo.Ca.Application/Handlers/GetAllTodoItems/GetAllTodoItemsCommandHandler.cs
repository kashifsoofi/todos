using MediatR;
using Todo.Domain.Aggregates.TodoItem;

namespace Todo.Ca.Application.Handlers.GetAllTodoItems;

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