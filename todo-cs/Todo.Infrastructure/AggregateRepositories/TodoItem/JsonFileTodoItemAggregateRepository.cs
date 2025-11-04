using System.Text;
using System.Text.Json;
using Todo.Domain.Aggregates.TodoItem;

namespace Todo.Infrastructure.AggregateRepositories.TodoItem;

public class JsonFileTodoItemAggregateRepository : ITodoItemAggregateRepository
{
    private readonly string _filePath;

    public JsonFileTodoItemAggregateRepository()
    {
        var dataHome = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
        if (string.IsNullOrEmpty(dataHome))
        {
            dataHome = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dataHome = Path.Combine(dataHome, ".local", "share", "todo");
        }

        if (!Directory.Exists(dataHome))
        {
            Directory.CreateDirectory(dataHome!);
        }

        _filePath = Path.Combine(dataHome!, "todo-store.json");
    }

    public List<TodoItemAggregate> GetAll()
    {
        return ReadData() ?? [];
    }

    public TodoItemAggregate GetById(string id)
    {
        var todoItems = ReadData() ?? [];
        return todoItems.FirstOrDefault(t => t.Id == id) ?? new TodoItemAggregate();
    }

    public void Create(TodoItemAggregate todoItem)
    {
        var todoItems = ReadData() ?? [];
        todoItems.Add(todoItem);
        WriteData(todoItems);
    }

    public void Update(TodoItemAggregate todoItem)
    {
        var todoItems = ReadData() ?? [];
        
        var todoItemToUpdate = todoItems.First(t => t.Id == todoItem.Id);
        todoItems.Remove(todoItemToUpdate);
        
        todoItems.Add(todoItem);
        WriteData(todoItems);
    }

    public void Delete(TodoItemAggregate todoItem)
    {
        var todoItems = ReadData() ?? [];
        todoItems.Remove(todoItem);
        WriteData(todoItems);
    }
    
    private List<TodoItemAggregate>? ReadData()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }
        
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoItemAggregate>>(jsonData);
    }

    private void WriteData(List<TodoItemAggregate> items)
    {
        var jsonData = JsonSerializer.Serialize(items);
        File.WriteAllText(_filePath, jsonData, Encoding.UTF8);
    }
}
