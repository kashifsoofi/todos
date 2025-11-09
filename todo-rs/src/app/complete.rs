use crate::domain::TodoItem;
use crate::infrastructure::TodoItemRepository;
use std::option::Option;

pub fn complete_todo_item(repository: &TodoItemRepository, id: String) -> Option<TodoItem> {
    let todo_item = repository.get_by_id(id);
    match todo_item {
        Some(mut todo_item) => {
            todo_item.is_complete = true;
            repository.update(todo_item.clone());
            Some(todo_item)
        },
        None => None,
    }
}
