using Todo.Domain;

namespace Todo.Infrastructure;

public interface ITodoRepository
{
    List<TodoItem> GetAll();
    TodoItem Add(TodoItem item);
    TodoItem GetById(string id);
    TodoItem Complete(string id);
    TodoItem Remove(string id);
}

public class TodoRepository : ITodoRepository
{
    public List<TodoItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public TodoItem Add(TodoItem item)
    {
        throw new NotImplementedException();
    }

    public TodoItem GetById(string id)
    {
        throw new NotImplementedException();
    }

    public TodoItem Complete(string id)
    {
        throw new NotImplementedException();
    }

    public TodoItem Remove(string id)
    {
        throw new NotImplementedException();
    }
}