using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Todo.Ca.Application;
using Todo.Ca.Cli.Actions;
using Todo.Ca.Cli.Commands;
using Todo.Ca.Infrastructure;

var services = new ServiceCollection()
    .AddTodoInfrastructure()
    .AddTodoServices()
    .AddSingleton<ListTodoItemsAction>()
    .AddSingleton<AddTodoItemAction>()
    .AddSingleton<CompleteTodoItemAction>()
    .AddSingleton<RemoveTodoItemAction>()
    .AddSingleton<Command, ListTodoItemsCommand>()
    .AddSingleton<Command, AddTodoItemCommand>()
    .AddSingleton<Command, CompleteTodoItemCommand>()
    .AddSingleton<Command, RemoveTodoItemCommand>()
    .AddSingleton<TodoCommand>();

var serviceProvider = services.BuildServiceProvider();

var rootCommand = serviceProvider.GetRequiredService<TodoCommand>();
var parseResult = rootCommand.Parse(args);
await parseResult.InvokeAsync();