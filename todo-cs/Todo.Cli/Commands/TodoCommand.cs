using System.CommandLine;

namespace Todo.Cli.Commands;

public class TodoCommand : RootCommand
{
    public TodoCommand(IEnumerable<Command> subCommands)
    {
        foreach (var subCommand in subCommands)
        {
            Add(subCommand);
        }
    }
}