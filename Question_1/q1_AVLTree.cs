using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_1
{
    internal class q1_AVLTree
    {
        // Currently a BST (need rotations and self balancing to make AVL
        // https://www.youtube.com/watch?v=Gt2yBZAhsGM&t=123s
        // Insert
        // Delete
        // Self Balance
        // In-order traversal
        // Search by year
        // Display the most recent book
        // Display the number of books

        // Necessary for this question:
        // Insert
        // Delete
        // Self Balance
        q1_AVLNode Root;
        public void Insert(q1_AVLNode Node)
        {            
            Root = InsertHelper(Root, Node);
        }
        private q1_AVLNode InsertHelper(q1_AVLNode Root, q1_AVLNode Node)
        {
            // Uses year to place nodes
            int year = Node.Book.Year;
            if (Root == null)
            {
                Root = Node;
                return Root;
            }
            else if (year < Root.Book.Year)
            {
                Root.Left = InsertHelper(Root.Left,Node);
            }
            else
            {
                Root.Right = InsertHelper(Root.Right, Node);
            }
            return Root;
        }
        public void InOrderTraversal() // Q2
        {
            InOrderTraversalHelper(Root);
        }
        private void InOrderTraversalHelper(q1_AVLNode Root)
        {
            if (Root != null)
            {
                InOrderTraversalHelper(Root.Left);
                Console.WriteLine($"{Root.Book.Title} - {Root.Book.Year}" );
                InOrderTraversalHelper(Root.Right); 
            }
        } //Q2
        public bool Search(int BookYear)
        {
            return SearchHelper(Root, BookYear);
        } // Left for Q2 - however, necessary for delete
        private bool SearchHelper(q1_AVLNode Root, int Year)
        {
            if (Root == null)
            {
                return false;
            } else if (Root.Book.Year == Year)
            {
                return true;
            } else if (Root.Book.Year > Year)
            {
                return SearchHelper(Root.Left, Year);
            }
            else
            {
                return SearchHelper(Root.Right, Year);
            }
        } // Q2
        public void Delete(int Year)
        {
            if (Search(Year))
            {
                DeleteHelper(Root, Year);
            }
            else
            {
                Console.WriteLine("Year '" + Year + "' could not be found");
            }
            
        }
        private q1_AVLNode DeleteHelper(q1_AVLNode Root, int Year)
        {
            if (Root == null)
            {
                return Root;
            }
            else if (Year < Root.Book.Year){
                Root.Left = DeleteHelper(Root.Left, Year);
            }
            else if (Year > Root.Book.Year)
            {
                Root.Right = DeleteHelper(Root.Right, Year);
            }
            else // Found node
            {
                // Check if leaf node
                if (Root.Left == null && Root.Right == null)
                {
                    Root = null;
                }
                else if (Root.Right != null) // Right child - need successor to replace node
                {
                    q1_AVLNode succ = Successor(Root);
                    Root.Book = succ.Book;
                    Root.Right = DeleteHelper(Root.Right,succ.Book.Year);
                }
                else
                {
                    q1_AVLNode pred = Predecessor(Root);
                    Root.Book = pred.Book;
                    Root.Left = DeleteHelper(Root.Left, pred.Book.Year);
                }
            }
                return Root;
        }
        private q1_AVLNode Successor(q1_AVLNode Node) // Find least value below the right child of this root node
        {
            q1_AVLNode current = Node.Right;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }
        private q1_AVLNode Predecessor(q1_AVLNode Node) // Fidn greatest value below the left child of this root node
        {
            q1_AVLNode current = Node.Left;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }
    }
}
