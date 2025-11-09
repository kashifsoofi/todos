use crate::domain::TodoItem;
use crate::infrastructure::TodoItemRepository;

pub fn list_todo_items(repository: &TodoItemRepository) -> Vec<TodoItem> {
    repository.get_all()
}
