using System.CommandLine;
using System.CommandLine.Invocation;
using Todo.Application;

namespace Todo.Cli.Actions;

public class ListTodoItemsAction(ITodoService todoService) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        var todoItems = todoService.GetAll();
        Console.WriteLine("Id       Name        Complete");
        foreach (var todoItem in  todoItems)
        {
            Console.WriteLine($"{todoItem.Id}   {todoItem.Name}     {todoItem.IsComplete}");
        }
        Console.WriteLine();
        Console.WriteLine($"Total: {todoItems.Count}, Complete: {todoItems.Count(x => x.IsComplete)}, Pending: {todoItems.Count(x => !x.IsComplete)}");

        return 0;
    }
}