using System.CommandLine;
using System.CommandLine.Invocation;
using MediatR;
using Todo.Application;
using Todo.Application.Handlers.GetAllTodoItems;

namespace Todo.Cli.Actions;

public class ListTodoItemsAction(IMediator mediator) : AsynchronousCommandLineAction
{
    public override async Task<int> InvokeAsync(ParseResult parseResult, CancellationToken cancellationToken = new CancellationToken())
    {
        var getAllTodoItemsResponse = await mediator.Send(new GetAllTodoItemsQuery(), cancellationToken);
        Console.WriteLine("Id       Name        Complete");
        var todoItems = getAllTodoItemsResponse.TodoItems;
        foreach (var todoItem in todoItems)
        {
            Console.WriteLine($"{todoItem.Id}   {todoItem.Name}     {todoItem.IsComplete}");
        }
        Console.WriteLine();
        Console.WriteLine($"Total: {todoItems.Count}, Complete: {todoItems.Count(x => x.IsComplete)}, Pending: {todoItems.Count(x => !x.IsComplete)}");
        
        return 0;
    }
}