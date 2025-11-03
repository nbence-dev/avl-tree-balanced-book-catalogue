using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Question_2
{
    internal class q2_AVLTree
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
        // AVL Tree and not BST
        q2_AVLNode Root; // Root node
        public void Insert(q2_AVLNode Node) // Public Insert Function
        {            
            Root = InsertHelper(Root, Node); // Will adapt Root node (return updated root node)
            if (Root != null) Root.Parent = null; // If there is a Root node, obviously it won't have a parent node
        }
        private q2_AVLNode InsertHelper(q2_AVLNode Root, q2_AVLNode Node) // Private Helper function for Insert function
        {
            
            
            if (Root == null) // If no node has been assigned to Root yet
            {
                Node.Left = Node.Right = null; // As there is no second node yet, therefore Left and Right need be empty.
                Node.Parent = null; 
                Node.Height = 1;
                return Node; // Return back to Root (Node is newly received Node user wants to insert)
            }
            int year = Node.Book.Year; // Need to find year to compare data and order nodes by value
            
            if (year < Root.Book.Year) // If new node's year is less than Root node's year
            {
                Root.Left = InsertHelper(Root.Left,Node); // Go to left subtree and re-compare values to determine where it must be placed in left subtree
                if (Root.Left != null) Root.Left.Parent = Root; // Update parent pointer after recursion, as rotations may have changed which node is at Root.Left
            }
            else
            {
                Root.Right = InsertHelper(Root.Right, Node);// Go to right subtree and re-compare values to determine where it must be placed in left subtree
                if (Root.Right != null) Root.Right.Parent = Root; // Update parent pointer after recursion, as rotations may have changed which node is at Root.Right
            }
            UpdateHeight(Root); 
            int balance = GetBalance(Root);

            // Left Left Case
            // Tree is left-heavy (balance > 1) and the new node fell into the left child's left subtree.
            // Fix by a single right rotation on Root.
            if (balance > 1 && year < Root.Left.Book.Year)
            {
                return RightRotate(Root);
            }
            // Right Right Case
            // Tree is right-heavy (balance < -1) and the new node fell into the right child's right subtree.
            // Fix by a single left rotation on Root.
            if (balance < -1 && year >= Root.Right.Book.Year)
            {
                return LeftRotate(Root);
            }
            // Left Right Case
            // Tree is left-heavy but the new node fell into the left child's right subtree.
            // Fix by rotating the left child left, then rotating Root right (double rotation).
            if (balance > 1 && year >= Root.Left.Book.Year)
            {
                Root.Left = LeftRotate(Root.Left);
                if (Root.Left !=null) Root.Left.Parent = Root;
                return RightRotate(Root);
            }
            // Right Left Case
            // Tree is right-heavy but the new node fell into the right child's left subtree.
            // Fix by rotating the right child right, then rotating Root left (double rotation).
            if (balance < -1 && year < Root.Right.Book.Year)
            {
                Root.Right = RightRotate(Root.Right);
                if (Root.Right != null) Root.Right.Parent = Root;
                return LeftRotate(Root) ; 
            }
            return Root;
        }
        public void InOrderTraversal() // Q2
        {
            InOrderTraversalHelper(Root);
        }
        private void InOrderTraversalHelper(q2_AVLNode Root)
        {
            if (Root != null)
            {
                // Uses recursion to firstly display most left nodes
                InOrderTraversalHelper(Root.Left);
                Console.WriteLine($"{Root.Book.Title} - {Root.Book.Year}" );
                // Then displays most right nodes
                InOrderTraversalHelper(Root.Right); 
                // Therefore, displays in ascending order
            }
        } //Q2
        public bool Search(int BookYear) 
        {
            return SearchHelper(Root, BookYear);
        } // Left for Q2 - however, necessary for delete
        private bool SearchHelper(q2_AVLNode Root, int Year)
        {
            if (Root == null) // If root == null, then no item could be found (false)
            {
                return false;
            } else if (Root.Book.Year == Year)
            {
                return true; // Item is within AVL tree
            } else if (Root.Book.Year > Year)
            {
                return SearchHelper(Root.Left, Year); // if year to search is smaller than root node's year, go left
            }
            else
            {
                return SearchHelper(Root.Right, Year); // if year to search is larger than root node's year, go right
            }
        } // Q2
        public void Delete(int Year) // Public delete function
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
        private q2_AVLNode DeleteHelper(q2_AVLNode Root, int Year)
        {
            if (Root == null) // Base: nothing to delete in an empty subtree.
            {
                return Root;
            }
            // Recurse to locate the node to delete using BST ordering by Year.
            if (Year < Root.Book.Year){ // Go in left subtree
                Root.Left = DeleteHelper(Root.Left, Year);
                if (Root.Left != null) Root.Left.Parent = Root;
            }
            else if (Year > Root.Book.Year)
            {
                // Go in right subtree
                Root.Right = DeleteHelper(Root.Right, Year);
                if (Root.Right != null) Root.Right.Parent = Root;
            }
            else // Found node
            {
                // Check if leaf node
                // Case 1: leaf node => simply remove it.
                if (Root.Left == null && Root.Right == null)
                {
                    return null;
                }
                // Case 2: node has a right child => replace with successor (smallest in right subtree).
                else if (Root.Right != null) // Right child - need successor to replace node
                {
                    q2_AVLNode succ = Successor(Root);
                    Root.Book = succ.Book; // copy successor's data
                    // delete successor from right subtree
                    Root.Right = DeleteHelper(Root.Right,succ.Book.Year);
                    if (Root.Right !=null) Root.Right.Parent = Root;
                }
                // Case 3: no right child but has left child => replace with predecessor (largest in left subtree).
                else
                {
                    q2_AVLNode pred = Predecessor(Root);
                    Root.Book = pred.Book; // copy predecessor's datas
                    // delete predecessor from left subtree
                    Root.Left = DeleteHelper(Root.Left, pred.Book.Year);
                    if (Root.Left != null) Root.Left.Parent = Root;
                }
            }
            // Update height and compute balance factor for AVL rebalancing.
            UpdateHeight(Root);
            int balance = GetBalance(Root);
            // Left heavy
            // If left-heavy after deletion, perform the appropriate rotation(s).
            if (balance > 1)
            {
                // Left-left
                // Left-Left case: left child is left-heavy or balanced -> single right rotation.
                if (GetBalance(Root.Left) >= 0)
                {
                    return RightRotate(Root);
                }
                // Left-Right
                // Left-Right case: left child is right-heavy -> left-rotate the left child then right-rotate Root.
                else
                {
                    Root.Left = LeftRotate(Root.Left);
                    if (Root.Left != null) Root.Left.Parent = Root;
                    return RightRotate(Root);
                }
            }
            // Right Heavy
            // If right-heavy after deletion, perform the appropriate rotation(s).
            if (balance < -1)
            {
                // Right-Right
                // Right-Right case: right child is right-heavy or balanced -> single left rotation.
                if (GetBalance(Root.Right) <= 0)
                {
                    return LeftRotate(Root);
                }
                //Right-left
                // Right-Left case: right child is left-heavy -> right-rotate the right child then left-rotate Root.
                else
                {
                    Root.Right = RightRotate(Root.Right);
                    if (Root.Right !=null) Root.Right.Parent = Root;
                    return LeftRotate(Root);
                }
            }
            // Nothing to rotate; return the (possibly updated) root.
            return Root;
        }
        private q2_AVLNode Successor(q2_AVLNode Node) // Find least value below the right child of this root node
        {
            q2_AVLNode current = Node.Right;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }
        private q2_AVLNode Predecessor(q2_AVLNode Node) // Find greatest value below the left child of this root node
        {
            q2_AVLNode current = Node.Left;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }

        // New functions for AVL..
        private int GetHeight(q2_AVLNode Node) => Node?.Height ?? 0; // Returns the height of a node, or  0 if the node is null (uses null-coalescing 

        // Recompute node height: 1 + max(height(left), height(right)).
        private void UpdateHeight(q2_AVLNode Node)
        {
            if (Node == null) return;
            Node.Height = 1 + Math.Max(GetHeight(Node.Left), GetHeight(Node.Right));
        }
        private int GetBalance(q2_AVLNode Node)// Return balance factor: height(left) - height(right). Positive => left-heavy.
        {
            if (Node == null) return 0;
            return GetHeight(Node.Left) - GetHeight(Node.Right);
        }

        // Perform a right rotation around node `y` and return the new subtree root (`x`).
        // Before:       y                 After:       x
        //              / \                            / \\
        //             x  T3                         T1  y
        //            / \                                / \\
        //          T1  T2                              T2  T3
        private q2_AVLNode RightRotate(q2_AVLNode y)
        {
            // x becomes new root of this subtree
            q2_AVLNode x = y.Left;
            // T2 will become y's left child after rotation
            q2_AVLNode T2 = x?.Right;

            // rotation
            x.Right = y;
            y.Left = T2;

            // Update parent references
            x.Parent = y.Parent;
            y.Parent =x;

            if (T2 != null) T2.Parent = y;

            // Update heights: update lower node first, then the new root
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        // Perform a left rotation around node `x` and return the new subtree root (`y`).
        // Before:     x                     After:       y
        //            / \                                / \\
        //          T1   y                              x  T3
        //              / \                            / \\
        //            T2  T3                         T1  T2
        private q2_AVLNode LeftRotate(q2_AVLNode x)
        {
            // y becomes new root of this subtree
            q2_AVLNode y = x.Right;
            // T2 will become x's right child after rotation
            q2_AVLNode T2 = y?.Left;

            //rotation
            y.Left = x;
            x.Right = T2;

            // Update parent references
            y.Parent = x.Parent;
            x.Parent = y;
            if (T2 != null) T2.Parent = x;

            // Update heights: update lower node first, then the new root
            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        public void RecentBook() // Searches for the most recent book by year 
        {
            q2_Book Book = RecentBookHelper(Root).Book;
            if (Book == null) // If book empty, no entries have been made
            {
                Console.WriteLine("No entries made");
                return;
            }
            
            Console.WriteLine(Book.Title); // Will display the title of the book
        }
        private q2_AVLNode RecentBookHelper(q2_AVLNode Root) // Takes the root node
        {
            if (Root == null) // if root null, return null
            {
                return null;
            }
            q2_AVLNode current = Root; // keep going right as the most recent would be the largest year
            while (current.Right != null)
            {
                current = current.Right;
            }

            // https://www.geeksforgeeks.org/dsa/find-the-node-with-maximum-value-in-a-binary-search-tree/
            return current;
        }

        public void NumberOfBooks() // Count number of books
        {
            int num = NumberOfBooksHelper(Root);
            if (num == 0)
            {
                Console.WriteLine("No entries made"); // if num == 0, then no books have been captured
                return;
            }
            Console.WriteLine($"Total number of books in AVL Tree: {num}"); // Print to console the total number of books
        }
        // https://www.geeksforgeeks.org/dsa/count-number-of-nodes-in-a-complete-binary-tree/
        private int NumberOfBooksHelper(q2_AVLNode Root)
        {
            if (Root == null) // if root null, tree is empty
            {
                return 0;
            }
            int l = NumberOfBooksHelper(Root.Left); // keep recursing to the left
            int r= NumberOfBooksHelper(Root.Right); // keep recursing to the right
            // Include the current node (+1) and return the sum.
            return 1 + l + r;
            
        }
    }
}
