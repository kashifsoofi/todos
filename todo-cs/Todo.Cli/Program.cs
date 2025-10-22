using System.CommandLine;

var listCommand = new Command("--list");
listCommand.SetAction(_ =>
{
    ListTodoItems();
    return 0;
});

Argument<string> nameArgument = new Argument<string>("name");
var addCommand = new Command("--add")
{
    nameArgument,
};
addCommand.SetAction(result =>
{
    var name = result.GetValue(nameArgument);
    AddTodoItem(name!);
    return 0;
});

Argument<int> idArgument = new Argument<int>("id");
var completeCommand = new Command("--complete")
{
    idArgument,
};
completeCommand.SetAction(parseResult =>
{
    var id = parseResult.GetValue(idArgument);
    CompleteTodoItem(id);
    return 0;
});

var removeCommand = new Command("--remove")
{
    idArgument,
};
removeCommand.SetAction(parseResult =>
{
    var id = parseResult.GetValue(idArgument);
    RemoveTodoItem(id);
    return 0;
});

RootCommand rootCommand = new("todo cli app")
{
    listCommand,
    addCommand,
    completeCommand,
    removeCommand,
};

ParseResult parseResult = rootCommand.Parse(args);
parseResult.Invoke();

static void ListTodoItems()
{
    
}

static void AddTodoItem(string name)
{
    
}

static void CompleteTodoItem(int id)
{
    
}

static void RemoveTodoItem(int id)
{
    
}

public record TodoItem(
    int Id,
    string Name,
    bool IsComplete);