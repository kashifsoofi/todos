using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Todo.Ca.Application.Handlers.CreateTodoItem;

namespace Todo.Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IMediator _mediator;

    public MainWindowViewModel(IMediator mediator)
    {
        _mediator = mediator;
        _newTodoItemName = string.Empty;

        if (Design.IsDesignMode)
        {
            TodoItems.Add(new TodoItemViewModel() { Name = "Hello" });
            TodoItems.Add(new TodoItemViewModel() { Name = "Avalonia" });
        }
    }

    public ObservableCollection<TodoItemViewModel> TodoItems { get; } = [];
    
    [RelayCommand(CanExecute = nameof(CanAddTodoItem))]
    private void AddTodoItem()
    {
        var itemCreated = _mediator.Send(new CreateTodoItemCommand(NewTodoItemName)).GetAwaiter().GetResult();
        TodoItems.Add(new TodoItemViewModel { Id = itemCreated.Id, Name = itemCreated.Name, IsComplete = itemCreated.IsComplete });
        
        NewTodoItemName = string.Empty;
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddTodoItemCommand))]
    private string _newTodoItemName;

    private bool CanAddTodoItem() => !string.IsNullOrWhiteSpace(NewTodoItemName);

    [RelayCommand]
    private void RemoveTodoItem(TodoItemViewModel item)
    {
        _ = _mediator.Send(new CreateTodoItemCommand(item.Id)).GetAwaiter().GetResult();
        TodoItems.Remove(item);
    }
}
