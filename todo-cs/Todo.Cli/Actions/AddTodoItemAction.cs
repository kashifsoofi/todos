using System.CommandLine;
using System.CommandLine.Invocation;
using Todo.Application;

namespace Todo.Cli.Actions;

public class AddTodoItemAction(TodoService todoService) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        throw new NotImplementedException();
    }
}