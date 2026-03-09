# avl-tree-balanced-book-catalogue

A self-balancing AVL Tree implementation in C# (.NET 8) that manages a catalogue of books, ordered by publication year. This project demonstrates core data structure concepts including AVL rotations, in-order traversal, search, deletion, and custom tree queries.

---

## 📚 Overview

Books are stored as nodes in an AVL Tree keyed by **publication year**. The tree automatically rebalances itself after every insert and delete using left/right rotations, ensuring O(log n) time complexity for all operations.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A terminal or an IDE such as [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio](https://visualstudio.microsoft.com/)

### Clone the Repository

```bash
git clone https://github.com/your-username/avl-tree-balanced-book-catalogue.git
cd avl-tree-balanced-book-catalogue
```

### Build the Project

```bash
dotnet build
```

### Run the Project

```bash
dotnet run --project Question_2
```

---

## 🗂️ Project Structure

```
AVLTree_PT2/
├── Question_2.sln
└── Question_2/
    ├── Program.cs          # Entry point — demo driver
    ├── q2_Book.cs          # Book model (Title, Author, Year)
    ├── q2_AVLNode.cs       # AVL Tree node (Book, Left, Right, Parent, Height)
    ├── q2_AVLTree.cs       # AVL Tree implementation
    └── Question_2.csproj
```

---

## ⚙️ How It Works

### Data Model

Each book stores three fields:

| Field   | Type   | Description            |
|---------|--------|------------------------|
| Title   | string | Title of the book      |
| Author  | string | Author of the book     |
| Year    | int    | Publication year (key) |

Books are keyed and ordered by `Year` within the AVL Tree.

### AVL Tree Operations

| Operation           | Method                  | Description                                                  |
|---------------------|-------------------------|--------------------------------------------------------------|
| **Insert**          | `Insert(node)`          | Inserts a book node and rebalances the tree                  |
| **Delete**          | `Delete(year)`          | Removes the node matching the given year and rebalances      |
| **Search**          | `Search(year)`          | Returns `true` if a book with that year exists               |
| **In-Order Traversal** | `InOrderTraversal()` | Prints all books in ascending year order                     |
| **Most Recent Book**| `RecentBook()`          | Displays the book with the highest publication year          |
| **Book Count**      | `NumberOfBooks()`       | Displays the total number of books in the tree               |

### Self-Balancing (AVL Rotations)

After every insert or delete, the tree checks the **balance factor** of each node (`height(left) - height(right)`). If the factor goes beyond `±1`, one of four rotations is applied:

- **Left-Left** → Single Right Rotation
- **Right-Right** → Single Left Rotation
- **Left-Right** → Left Rotation on child, then Right Rotation on root
- **Right-Left** → Right Rotation on child, then Left Rotation on root

---

## 🧪 Sample Output

Running the program produces output similar to the following:

```
Inserting...
Insert was successful.

Displaying in order...
Pride and Prejudice - 1813
Moby-Dick - 1851
Wuthering Heights - 1847
...
The Hunger Games - 2008

Searching for 1977...
True

Deleting node '1977'...
Displaying in order...
Pride and Prejudice - 1813
...
(1977 / The Shining no longer present)

Displaying most recent book by year...
The Hunger Games

Counting total number of books entered...
Total number of books in AVL Tree: 19
```

---

## 📖 Sample Book Catalogue

The demo pre-loads 20 classic and contemporary books, including:

- *The Great Gatsby* — F. Scott Fitzgerald (1925)
- *1984* — George Orwell (1949)
- *The Hobbit* — J.R.R. Tolkien (1937)
- *Harry Potter and the Sorcerer's Stone* — J.K. Rowling (1997)
- *The Silent Patient* — Alex Michaelides (2019)

---

## 🛠️ Technologies Used

- **C#** (.NET 8)
- **AVL Tree** (self-balancing BST)
- **Recursive algorithms** (insert, delete, traversal, search)
- **OOP principles** (encapsulation, separation of concerns)

---

## 📄 License

This project is for educational purposes.

