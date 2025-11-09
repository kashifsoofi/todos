using System.CommandLine;
using Todo.Ca.Cli.Actions;

namespace Todo.Ca.Cli.Commands;

public class CompleteTodoItemCommand : Command
{
    private readonly Argument<string> _idArgument = new ("id");

    public CompleteTodoItemCommand(CompleteTodoItemAction action) : base("complete", "Complete todo item")
    {
        Arguments.Add(_idArgument);
        
        Action = action;
    }
}