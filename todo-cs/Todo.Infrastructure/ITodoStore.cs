using Todo.Domain;

namespace Todo.Infrastructure;

public interface ITodoStore
{
    List<TodoItem> GetAll();
    TodoItem? Add(TodoItem item);
    TodoItem? Complete(string id);
    TodoItem? Remove(string id);
}