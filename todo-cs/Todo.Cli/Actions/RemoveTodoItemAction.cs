using System.CommandLine;
using System.CommandLine.Invocation;
using Todo.Application;

namespace Todo.Cli.Actions;

public class RemoveTodoItemAction(ITodoService todoService) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        var id = parseResult.GetRequiredValue<string>("id");
        var removed = todoService.Remove(id);
        if (removed == null)
        {
            Console.WriteLine($"No item found to remove with id: {id}");
            return 1;
        }
        
        Console.WriteLine("Todo item removed from list");
        Console.WriteLine($"Id: {removed.Id}, Name: {removed.Name}, Complete: {removed.IsComplete}");
        return 0;
    }
}