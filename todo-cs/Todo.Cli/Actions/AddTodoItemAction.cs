using System.CommandLine;
using System.CommandLine.Invocation;
using MediatR;
using Todo.Application;
using Todo.Application.Handlers.CreateTodoItem;
using Todo.Cli.Commands;
using Todo.Domain.Aggregates.TodoItem.Commands;

namespace Todo.Cli.Actions;

public class AddTodoItemAction(IMediator mediator) : AsynchronousCommandLineAction
{
    public override async Task<int> InvokeAsync(ParseResult parseResult, CancellationToken cancellationToken = new())
    {
        var name = parseResult.GetRequiredValue<string>("name");
        var added = await mediator.Send(new CreateTodoItemCommand(name), cancellationToken);
        if (added == null)
        {
            Console.WriteLine("Error adding todo item");
            return 1;
        }
        
        Console.WriteLine("Todo item added to list");
        Console.WriteLine($"Id: {added.Id}, Name: {added.Name}");
        return 0;
    }
}