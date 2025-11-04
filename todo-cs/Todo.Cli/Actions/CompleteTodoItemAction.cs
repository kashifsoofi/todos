using System.CommandLine;
using System.CommandLine.Invocation;
using MediatR;
using Todo.Application;
using Todo.Application.Handlers.CompleteTodoItem;

namespace Todo.Cli.Actions;

public class CompleteTodoItemAction(IMediator mediator) : AsynchronousCommandLineAction
{
    public override async Task<int> InvokeAsync(ParseResult parseResult, CancellationToken cancellationToken = new())
    {
        var id = parseResult.GetRequiredValue<string>("id");
        var completed = await mediator.Send(new CompleteTodoItemCommand(id), cancellationToken);
        if (completed == null)
        {
            Console.WriteLine($"No item found to complete with id: {id}");
            return 1;
        }
        
        Console.WriteLine("Todo item completed");
        Console.WriteLine($"Id: {completed.Id}, Name: {completed.Name}");
        return 0;
    }
}