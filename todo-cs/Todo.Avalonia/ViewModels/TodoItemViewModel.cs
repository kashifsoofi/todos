using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Todo.Domain;

namespace Todo.Avalonia.ViewModels;

public partial class TodoItemViewModel : ViewModelBase
{
    [ObservableProperty] private string _id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private bool _isComplete;

    public TodoItemViewModel()
    {
        _id = Guid.NewGuid().ToString("N")[..5];
        _isComplete = false;
    }

    public TodoItemViewModel(TodoItem todoItem)
    {
        _id = todoItem.Id;
        _name = todoItem.Name;
        _isComplete = todoItem.IsComplete;
    }
    
    public TodoItem GetTodoItem()
    {
        return new TodoItem(Id, Name, IsComplete);
    }
}