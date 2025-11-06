use serde::{Deserialize, Serialize};

#[derive(Serialize, Deserialize, Debug, Clone)]
pub struct TodoItem {
    pub id: String,
    pub name: String,
    pub is_complete: bool,
}
