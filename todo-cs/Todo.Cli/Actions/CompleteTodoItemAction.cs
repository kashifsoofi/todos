using System.CommandLine;
using System.CommandLine.Invocation;
using Todo.Application;

namespace Todo.Cli.Actions;

public class CompleteTodoItemAction(ITodoService todoService) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        var id = parseResult.GetRequiredValue<string>("id");
        var completed = todoService.Complete(id);
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