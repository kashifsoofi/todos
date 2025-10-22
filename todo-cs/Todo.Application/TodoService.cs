using Todo.Domain;
using Todo.Infrastructure;

namespace Todo.Application;

public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem Add(string name);
    TodoItem GetById(string id);
    TodoItem Complete(string id);
    TodoItem Remove(string id);
}

public class TodoService(ITodoRepository repository) : ITodoService
{
    public List<TodoItem> GetAll()
    {
        return repository.GetAll();
    }

    public TodoItem Add(string name)
    {
        var id = Guid.NewGuid().ToString("N").Substring(0, 5);
        var todoItem = new TodoItem(id, name, false);
        return repository.Add(todoItem);
    }

    public TodoItem GetById(string id)
    {
        return repository.GetById(id);
    }

    public TodoItem Complete(string id)
    {
        return repository.Complete(id);
    }

    public TodoItem Remove(string id)
    {
        return repository.Remove(id);
    }
}