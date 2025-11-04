namespace Todo.Domain.Aggregates.TodoItem;

public interface ITodoItemAggregateRepository
{
    List<TodoItemAggregate> GetAll();
    TodoItemAggregate GetById(string id);
    void Create(TodoItemAggregate todoItem);
    void Update(TodoItemAggregate todoItem);
    void Delete(TodoItemAggregate todoItem);
}
