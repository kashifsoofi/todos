using System;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Todo.Avalonia.ViewModels;
using Todo.Avalonia.Views;
using Todo.Ca.Application;
using Todo.Ca.Infrastructure;
using Todo.Domain.Aggregates.TodoItem;

namespace Todo.Avalonia;

public partial class App : global::Avalonia.Application
{
    private IServiceProvider _serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection()
            .AddTodoServices()
            .AddTodoInfrastructure()
            .AddSingleton<MainWindowViewModel>();

        _serviceProvider = collection.BuildServiceProvider();
        
        var vm = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm,
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();

        InitMainWindowViewModel();
    }

    private void InitMainWindowViewModel()
    {
        var repository = _serviceProvider.GetRequiredService<ITodoItemAggregateRepository>();
        var savedTodoItems = repository.GetAll();

        if (savedTodoItems.Count > 0)
        {
            var vm = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            
            foreach (var todoItem in savedTodoItems)
            {
                vm.TodoItems.Add(new TodoItemViewModel(todoItem));
            }
        }
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
    
    private bool _canClose;

    private void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        // e.Cancel = !_canClose;
        //
        // if (!_canClose)
        // {
        //     var vm = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        //     var itemsToSave = vm.TodoItems.Select(x => x.GetTodoItem()).ToList();
        //
        //     var todoStore = new JsonFileTodoItemAggregateRepository();
        //     // todoStore.SaveAll(itemsToSave);
        //     
        //     _canClose = true;
        //     if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //     {
        //         desktop.Shutdown();
        //     }
        // }
    }
}