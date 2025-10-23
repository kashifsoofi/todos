using System.CommandLine;
using Todo.Cli.Actions;

namespace Todo.Cli.Commands;

public class RemoveTodoItemCommand : Command
{
    private readonly Argument<string> _idArgument = new ("id");
    
    public RemoveTodoItemCommand(RemoveTodoItemAction action) : base("--remove", "Remove todo item")
    {
        Aliases.Add("-r");
        Arguments.Add(_idArgument);
        
        Action = action;
    }
}