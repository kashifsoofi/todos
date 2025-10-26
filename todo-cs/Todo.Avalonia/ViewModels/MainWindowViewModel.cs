using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Todo.Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
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
        TodoItems.Add(new TodoItemViewModel { Name = NewTodoItemName });
        NewTodoItemName = string.Empty;
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddTodoItemCommand))]
    private string _newTodoItemName;

    private bool CanAddTodoItem() => !string.IsNullOrWhiteSpace(NewTodoItemName);

    [RelayCommand]
    private void RemoveTodoItem(TodoItemViewModel item)
    {
        TodoItems.Remove(item);
    }
}
