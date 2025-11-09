use crate::domain::TodoItem;
use crate::infrastructure::TodoItemRepository;

pub fn todo_items_list(repository: &TodoItemRepository) -> Vec<TodoItem> {
    repository.get_all()
}
