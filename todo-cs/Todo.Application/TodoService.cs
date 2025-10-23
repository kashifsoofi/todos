using Todo.Domain;
using Todo.Infrastructure;

namespace Todo.Application;

public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem? Add(string name);
    TodoItem? Complete(string id);
    TodoItem? Remove(string id);
}

public class TodoService(ITodoStore store) : ITodoService
{
    public List<TodoItem> GetAll()
    {
        return store.GetAll();
    }

    public TodoItem? Add(string name)
    {
        var id = Guid.NewGuid().ToString("N").Substring(0, 5);
        var todoItem = new TodoItem(id, name, false);
        return store.Add(todoItem);
    }
    
    public TodoItem? Complete(string id)
    {
        return store.Complete(id);
    }

    public TodoItem? Remove(string id)
    {
        return store.Remove(id);
    }
}