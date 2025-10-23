using System.CommandLine;
using System.CommandLine.Invocation;
using Todo.Application;

namespace Todo.Cli.Actions;

public class AddTodoItemAction(ITodoService todoService) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        var name = parseResult.GetRequiredValue<string>("name");
        var added = todoService.Add(name);
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