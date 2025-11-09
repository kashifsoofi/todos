use crate::domain::TodoItem;
use crate::infrastructure::TodoItemRepository;

pub fn create_todo_item(repository: &TodoItemRepository, name: String) -> TodoItem {
    let id = uuid::Uuid::new_v4().to_string()[0..5].to_string();
    let todo_item = TodoItem::new(id, name, false);
    repository.create(todo_item.clone());
    todo_item
}
