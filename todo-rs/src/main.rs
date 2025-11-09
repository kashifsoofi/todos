mod domain;
mod infrastructure;
mod app;

use app::{list_todo_items, create_todo_item, complete_todo_item, remove_todo_item};
use infrastructure::TodoItemRepository;

use clap::{Parser, Subcommand};

#[derive(Parser, Debug)]
#[command(
    name = "domain-cli",
    version = "0.1.0",
    author = "Your Name",
    about = "Simple domain items cli tool for learning rust",
    arg_required_else_help = true,
)]
struct Cli {
    #[command(subcommand)]
    command: Option<Commands>,
}

#[derive(Subcommand, Debug)]
enum Commands {
    List,
    Add {
        name: String,
    },
    Complete {
        id: String,
    },
    Remove {
        id: String,
    },
}

fn main() {
    let cli = Cli::parse();
    let repository = TodoItemRepository::new();

    match cli.command {
        Some(Commands::List) => list(repository),
        Some(Commands::Add { name }) => {
            let item = create_todo_item(&repository, name);
            println!("Created todo item, id:{}, name:{}, is_complete: false{}", item.id, item.name, item.is_complete);
        },
        Some(Commands::Complete { id }) => {
            let item = complete_todo_item(&repository, id.to_string());
            match item {
                Some(item) => println!("Completed todo item, id:{}, name:{}, is_complete: false{}", item.id, item.name, item.is_complete),
                None => eprintln!("No todo item found with id {}", id),
            }
        },
        Some(Commands::Remove { id }) => {
            let item = remove_todo_item(&repository, id.to_string());
            match item {
                Some(item) => println!("Removed todo item, id:{}, name:{}, is_complete: false{}", item.id, item.name, item.is_complete),
                None => eprintln!("No todo item found with id {}", id),
            }
        },
        None => {},
    }
}

fn list(repository: TodoItemRepository) {
    let items = list_todo_items(&repository);
    for item in items {
        println!("{}    {}      {}", item.id, item.name, item.is_complete);
    }
}
