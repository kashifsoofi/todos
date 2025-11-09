using System.CommandLine;
using Todo.Ca.Cli.Actions;

namespace Todo.Ca.Cli.Commands;

public class ListTodoItemsCommand : Command
{
    public ListTodoItemsCommand(ListTodoItemsAction action) : base("list", "List all todo items")
    {
        Action = action;
    }
}