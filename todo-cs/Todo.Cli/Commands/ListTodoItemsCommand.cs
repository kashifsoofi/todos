using System.CommandLine;
using Todo.Cli.Actions;

namespace Todo.Cli.Commands;

public class ListTodoItemsCommand : Command
{
    public ListTodoItemsCommand(ListTodoItemsAction action) : base("--list", "List all todo items")
    {
        Action = action;
    }
}