use std::path::PathBuf;
use crate::domain::TodoItem;
use directories::BaseDirs;
use std::fs;
use serde::{Deserialize, Serialize};

pub struct TodoItemRepository {
    file_path: PathBuf,
}

impl TodoItemRepository {
    pub fn new() -> Self {
        let data_dir = get_data_dir();
        fs::create_dir_all(&data_dir).unwrap();

        let file_path = data_dir.join("todo-store.json");
        Self {
            file_path,
        }
    }

    pub fn get_all(&self) -> Vec<TodoItem> {
        self.read_data()
    }

    pub fn get_by_id(&self, id: String) -> Option<TodoItem> {
        let todo_items = self.read_data();
        todo_items.into_iter().find(|item| item.id == id)
    }

    pub fn create(&self, todo_item: TodoItem) {
        let mut todo_items = self.read_data();
        todo_items.push(todo_item);
        self.write_data(todo_items);
    }

    pub fn update(&self, todo_item: TodoItem) {
        let mut todo_items = self.read_data();
        let index = todo_items.iter().position(|item| item.id == todo_item.id).unwrap();
        todo_items[index] = todo_item;
        self.write_data(todo_items);
    }

    pub fn remove(&self, todo_item: TodoItem) {
        let mut todo_items = self.read_data();
        let index = todo_items.iter().position(|item| item.id == todo_item.id).unwrap();
        todo_items.remove(index);
        self.write_data(todo_items);
    }

    fn read_data(&self) -> Vec<TodoItem> {
        if !fs::metadata(self.file_path.as_path()).is_ok() {
            return vec![];
        }

        let json_data = fs::read_to_string(self.file_path.as_path()).unwrap();
        let dto_list = serde_json::from_str::<Vec<TodoItemDto>>(&json_data);
        match dto_list {
            Ok(r) => r.into_iter().map(|dto| TodoItem::new(dto.id, dto.name, dto.is_complete)).collect(),
            Err(e) => {
                println!("Error reading data: {}", e);
                vec![]
            }
        }
    }

    fn write_data(&self, todo_items: Vec<TodoItem>) {
        let dto_list: Vec<TodoItemDto> = todo_items.into_iter().map(|item| TodoItemDto { id: item.id, name: item.name, is_complete: item.is_complete }).collect();
        let json_data = serde_json::to_string_pretty(&dto_list).unwrap();
        fs::write(self.file_path.as_path(), json_data).unwrap();
    }
}

fn get_data_dir() -> PathBuf {
    let base_dirs = BaseDirs::new().unwrap();
    base_dirs.data_local_dir().join("todo")
}

#[derive(Serialize, Deserialize, Debug)]
#[serde(rename_all = "camelCase")]
struct TodoItemDto {
    id: String,
    name: String,
    is_complete: bool,
}