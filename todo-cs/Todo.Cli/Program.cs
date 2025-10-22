using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application;
using Todo.Cli.Actions;
using Todo.Cli.Commands;
using Todo.Infrastructure;

var services = new ServiceCollection()
    .AddTodoInfrastructure()
    .AddTodoServices()
    .AddSingleton<ListTodoItemsAction>()
    .AddSingleton<AddTodoItemAction>()
    .AddSingleton<CompleteTodoItemAction>()
    .AddSingleton<RemoveTodoItemAction>()
    .AddSingleton<ListTodoItemsCommand>()
    .AddSingleton<AddTodoItemCommand>()
    .AddSingleton<CompleteTodoItemCommand>()
    .AddSingleton<RemoveTodoItemCommand>()
    .AddSingleton<TodoCommand>();

var serviceProvider = services.BuildServiceProvider();

var rootCommand = serviceProvider.GetRequiredService<TodoCommand>();
var parseResult = rootCommand.Parse(args);
parseResult.Invoke();
    
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

RootCommand rootCommand1 = new("todo cli app")
{
    listCommand,
    addCommand,
    completeCommand,
    removeCommand,
};

ParseResult parseResult1 = rootCommand1.Parse(args);
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

