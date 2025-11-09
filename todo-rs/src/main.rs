mod domain;
mod infrastructure;
mod app;

use app::todo_items_list;
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

    match cli.command {
        Some(Commands::List) => list(),
        Some(Commands::Add { name }) => println!("adding new node: {}", name),
        Some(Commands::Complete { id }) => println!("completing {}", id),
        Some(Commands::Remove { id }) => println!("removing {}", id),
        None => {},
    }
}

fn list() {
    let repository = TodoItemRepository::new();

    let items = todo_items_list(&repository);
    for item in items {
        println!("{}    {}      {}", item.id, item.name, item.is_complete);
    }
}