pub struct TodoItem {
    pub id: String,
    pub name: String,
    pub is_complete: bool,
}

impl TodoItem {
    pub fn new(id: String, name: String, is_complete: bool) -> Self {
        TodoItem {
            id,
            name,
            is_complete,
        }
    }

    pub fn new(name: String) -> Self {
        new(String::from("0"), name, false)
    }
}