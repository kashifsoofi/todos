use crate::domain::TodoItem;
use crate::infrastructure::TodoItemRepository;
use std::option::Option;

pub fn remove_todo_item(repository: &TodoItemRepository, id: String) -> Option<TodoItem> {
    let todo_item = repository.get_by_id(id);
    match todo_item {
        Some(todo_item) => {
            repository.remove(todo_item.clone());
            Some(todo_item)
        },
        None => None,
    }
}
