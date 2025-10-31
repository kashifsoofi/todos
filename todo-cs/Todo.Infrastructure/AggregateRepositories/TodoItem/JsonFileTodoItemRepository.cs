using Todo.Domain.Aggregates.TodoItem;

namespace Todo.Infrastructure.AggregateRepositories.TodoItem;

public class JsonFileTodoItemRepository : ITodoItemRepository
{
    private readonly string _filePath;

    public JsonFileTodoItemRepository()
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
}
