using System.CommandLine;
using Todo.Cli.Actions;

namespace Todo.Cli.Commands;

public class CompleteTodoItemCommand : Command
{
    private readonly Argument<string> _idArgument = new ("id");

    public CompleteTodoItemCommand(CompleteTodoItemAction action) : base("--complete", "Complete todo item")
    {
        Aliases.Add("-c");
        Arguments.Add(_idArgument);
        
        Action = action;
    }
}