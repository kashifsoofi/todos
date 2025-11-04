using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Todo.Avalonia.ViewModels;
using Todo.Avalonia.Views;
using Todo.Infrastructure;
using Todo.Infrastructure.AggregateRepositories.TodoItem;

namespace Todo.Avalonia;

public partial class App : global::Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private readonly MainWindowViewModel _mainWindowViewModel = new();

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();

        InitMainWindowViewModel();
    }

    private void InitMainWindowViewModel()
    {
        var todoStore = new JsonFileTodoItemAggregateRepository();
        var savedTodoItems = todoStore.GetAll();

        if (savedTodoItems.Count > 0)
        {
            foreach (var todoItem in savedTodoItems)
            {
                _mainWindowViewModel.TodoItems.Add(new TodoItemViewModel(todoItem));
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
        e.Cancel = !_canClose;

        if (!_canClose)
        {
            var itemsToSave = _mainWindowViewModel.TodoItems.Select(x => x.GetTodoItem()).ToList();

            var todoStore = new JsonFileTodoItemAggregateRepository();
            // todoStore.SaveAll(itemsToSave);
            
            _canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}