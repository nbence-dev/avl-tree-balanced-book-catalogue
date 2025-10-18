namespace Question_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sample data instantiated
            List<q1_Book> listBooks = InstantiateList();
            // Creation of AVLTree
            q1_AVLTree AVLTree = new();
            // User feedback
            Console.WriteLine("Inserting...");
            Thread.Sleep(1000);
            // Insert each book in sample order (according to year)
            foreach (q1_Book book in listBooks)
            {
                AVLTree.Insert(new q1_AVLNode(book));
            }
            Console.WriteLine("Insert was successful.\n");
            Console.WriteLine("Displaying in order...");
            Thread.Sleep(1000);
            // Display each book in order (ascending)
            AVLTree.InOrderTraversal();
            Console.WriteLine("\nSearching for 1977...");
            Thread.Sleep(1000);
            // Search for year 1977 (should return true)
            Console.WriteLine(AVLTree.Search(1977));

            // Deleting node 1977
            Console.WriteLine("\nDeleting node '1977'...");
            Thread.Sleep(1000);

            AVLTree.Delete(1977);
            Console.WriteLine("Displaying in order...");
            Thread.Sleep(1000);
            // Display each book in order (ascending)
            // Displaying again
            AVLTree.InOrderTraversal();

            // Display most recent book
            Console.WriteLine("\nDisplaying most recent book by year...");
            Thread.Sleep(1000);
            AVLTree.RecentBook();

            // Display total number of nodes
            Console.WriteLine("\nCounting total number of books entered...");
            Thread.Sleep(1000);
            AVLTree.NumberOfBooks();

        }
        // For Code Cleanup
        static List<q1_Book> InstantiateList()
        {
            List<q1_Book> listBooks = new List<q1_Book>
            {
            new q1_Book("The Silent Patient", "Alex Michaelides", 2019),
            new q1_Book("The Great Gatsby", "F. Scott Fitzgerald", 1925),
            new q1_Book("To Kill a Mockingbird", "Harper Lee", 1960),
            new q1_Book("1984", "George Orwell", 1949),
            new q1_Book("The Catcher in the Rye", "J.D. Salinger", 1951),
            new q1_Book("Pride and Prejudice", "Jane Austen", 1813),
            new q1_Book("Moby-Dick", "Herman Melville", 1851),
            new q1_Book("The Hobbit", "J.R.R. Tolkien", 1937),
            new q1_Book("Brave New World", "Aldous Huxley", 1932),
            new q1_Book("The Book Thief", "Markus Zusak", 2005),
            new q1_Book("The Road", "Cormac McCarthy", 2006),
            new q1_Book("Harry Potter and the Sorcerer's Stone", "J.K. Rowling", 1997),
            new q1_Book("The Girl with the Dragon Tattoo", "Stieg Larsson", 2005),
            new q1_Book("The Alchemist", "Paulo Coelho", 1988),
            new q1_Book("The Shining", "Stephen King", 1977),
            new q1_Book("Wuthering Heights", "Emily Brontë", 1847),
            new q1_Book("Catch-22", "Joseph Heller", 1961),
            new q1_Book("The Hunger Games", "Suzanne Collins", 2008),
            new q1_Book("The Da Vinci Code", "Dan Brown", 2003),
            new q1_Book("The Outsiders", "S.E. Hinton", 1967)

            };
            return listBooks;
        }
    }
}
