using System.CommandLine;
using System.CommandLine.Invocation;
using MediatR;
using Todo.Application;
using Todo.Application.Handlers.RemoveTodoItem;

namespace Todo.Cli.Actions;

public class RemoveTodoItemAction(IMediator mediator) : AsynchronousCommandLineAction
{
    public override async Task<int> InvokeAsync(ParseResult parseResult, CancellationToken cancellationToken = new())
    {
        var id = parseResult.GetRequiredValue<string>("id");
        var removed = await mediator.Send(new RemoveTodoItemCommand(id), cancellationToken);
        if (removed == null)
        {
            Console.WriteLine($"No item found to complete with id: {id}");
            return 1;
        }
        
        Console.WriteLine("Todo item completed");
        Console.WriteLine($"Id: {removed.Id}, Name: {removed.Name}");
        return 0;
    }
}