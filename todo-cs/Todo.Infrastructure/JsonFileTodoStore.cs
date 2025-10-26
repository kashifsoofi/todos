using System.Text;
using System.Text.Json;
using Todo.Domain;

namespace Todo.Infrastructure;

public class JsonFileTodoStore : ITodoStore
{
    private readonly string _filePath;

    public JsonFileTodoStore()
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

    private List<TodoItem>? ReadData()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }
        
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoItem>>(jsonData);
    }

    private void WriteData(List<TodoItem> items)
    {
        var jsonData = JsonSerializer.Serialize(items);
        File.WriteAllText(_filePath, jsonData, Encoding.UTF8);
    }

    public void SaveAll(List<TodoItem> items)
    {
        WriteData(items);
    }
    
    public List<TodoItem> GetAll()
    {
        return ReadData() ?? [];
    }

    public TodoItem? Add(TodoItem item)
    {
        var todoItems = ReadData() ?? [];
        todoItems.Add(item);
        WriteData(todoItems);
        return item;
    }

    public TodoItem? Complete(string id)
    {
        var todoItems = ReadData() ?? [];
        var todoItem =  todoItems.FirstOrDefault(t => t.Id == id);
        if (todoItem == null)
        {
            return null;
        }

        todoItem.IsComplete = true;
        WriteData(todoItems);

        return todoItem;
    }

    public TodoItem? Remove(string id)
    {
        var todoItems = ReadData() ?? [];
        var todoItem =  todoItems.FirstOrDefault(t => t.Id == id);
        if (todoItem == null)
        {
            return null;
        }

        todoItems.Remove(todoItem);
        WriteData(todoItems);
        return todoItem;
    }
}