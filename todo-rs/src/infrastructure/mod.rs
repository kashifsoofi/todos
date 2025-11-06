use std::env;
use crate::domain::TodoItem;

pub struct TodoItemRepository {
    // domain: Vec<TodoItem>,
}

impl TodoItemRepository {
    pub fn new() -> Self {
        let data_home = env::var("XDG_DATA");
        let data_home = match env::var("XDG_DATA") {
            Ok(value) => value,
            Err(_) => {
                
            }
        }
        }
        if Some(data_home) = data_home {
            println!(")
        if let !Ok(data_home) = env::var("XDG_DATA_HOME") {
            println!("data home: {}", data_home);
        }

        var dataHome = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
        if (string.IsNullOrEmpty(dataHome))
        @@ -25,67 +25,56 @@ public class JsonFileTodoStore : ITodoStore
        _filePath = Path.Combine(dataHome!, "todo-store.json");
    }
    pub fn list(&self) -> Vec<TodoItem> {
    }
}