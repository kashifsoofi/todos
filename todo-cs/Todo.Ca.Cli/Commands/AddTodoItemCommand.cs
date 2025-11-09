using System.CommandLine;
using Todo.Ca.Cli.Actions;

namespace Todo.Ca.Cli.Commands;

public class AddTodoItemCommand : Command
{
    private readonly Argument<string> _nameArgument = new ("name");
    
    public AddTodoItemCommand(AddTodoItemAction action) : base("add", "Add todo item")
    {
        Arguments.Add(_nameArgument);
        
        Action = action;
    }
}