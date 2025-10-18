using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
        // AVL Tree and not BST
        q1_AVLNode Root;
        public void Insert(q1_AVLNode Node)
        {            
            Root = InsertHelper(Root, Node);
            if (Root != null) Root.Parent = null;
        }
        private q1_AVLNode InsertHelper(q1_AVLNode Root, q1_AVLNode Node)
        {
            
            
            if (Root == null)
            {
                Node.Left = Node.Right = null;
                Node.Parent = null;
                Node.Height = 1;
                return Node;
            }
            int year = Node.Book.Year;
            
            if (year < Root.Book.Year)
            {
                Root.Left = InsertHelper(Root.Left,Node);
                if (Root.Left != null) Root.Left.Parent = Root;
            }
            else
            {
                Root.Right = InsertHelper(Root.Right, Node);
                if (Root.Right != null) Root.Right.Parent = Root;
            }
            UpdateHeight(Root);
            int balance = GetBalance(Root);

            // Left Left
            if (balance > 1 && year < Root.Left.Book.Year)
            {
                return RightRotate(Root);
            }
            // Right Right
            if (balance < -1 && year >= Root.Right.Book.Year)
            {
                return LeftRotate(Root);
            }
            // Left Right
            if (balance > 1 && year >= Root.Left.Book.Year)
            {
                Root.Left = LeftRotate(Root.Left);
                if (Root.Left !=null) Root.Left.Parent = Root;
                return RightRotate(Root);
            }
            // Right Left
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
            if (Year < Root.Book.Year){
                Root.Left = DeleteHelper(Root.Left, Year);
                if (Root.Left != null) Root.Left.Parent = Root;
            }
            else if (Year > Root.Book.Year)
            {
                Root.Right = DeleteHelper(Root.Right, Year);
                if (Root.Right != null) Root.Right.Parent = Root;
            }
            else // Found node
            {
                // Check if leaf node
                if (Root.Left == null && Root.Right == null)
                {
                    return null;
                }
                else if (Root.Right != null) // Right child - need successor to replace node
                {
                    q1_AVLNode succ = Successor(Root);
                    Root.Book = succ.Book;
                    Root.Right = DeleteHelper(Root.Right,succ.Book.Year);
                    if (Root.Right !=null) Root.Right.Parent = Root;
                }
                else
                {
                    q1_AVLNode pred = Predecessor(Root);
                    Root.Book = pred.Book;
                    Root.Left = DeleteHelper(Root.Left, pred.Book.Year);
                    if (Root.Left != null) Root.Left.Parent = Root;
                }
            }
            UpdateHeight(Root);
            int balance = GetBalance(Root);
            // Left heavy
            if (balance > 1)
            {
                // Left-left
                if (GetBalance(Root.Left) >= 0)
                {
                    return RightRotate(Root);
                }
                // Left-Right
                else
                {
                    Root.Left = LeftRotate(Root.Left);
                    if (Root.Left != null) Root.Left.Parent = Root;
                    return RightRotate(Root);
                }
            }
            // Right Heavy
            if (balance < -1)
            {
                // Right-Right
                if(GetBalance(Root.Right) <= 0)
                {
                    return LeftRotate(Root);
                }
                //Right-left
                else
                {
                    Root.Right = RightRotate(Root.Right);
                    if (Root.Right !=null) Root.Right.Parent = Root;
                    return LeftRotate(Root);
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

        // New functions for AVL..
        private int GetHeight(q1_AVLNode Node) => Node?.Height ?? 0;

        private void UpdateHeight(q1_AVLNode Node)
        {
            if (Node == null) return;
            Node.Height = 1 + Math.Max(GetHeight(Node.Left), GetHeight(Node.Right));
        }
        private int GetBalance(q1_AVLNode Node)
        {
            if (Node == null) return 0;
            return GetHeight(Node.Left) - GetHeight(Node.Right);
        }

        private q1_AVLNode RightRotate(q1_AVLNode y)
        {
            q1_AVLNode x = y.Left;
            q1_AVLNode T2 = x?.Right;

            // rotation
            x.Right = y;
            y.Left = T2;

            //parents
            x.Parent = y.Parent;
            y.Parent =x;

            if (T2 != null) T2.Parent = y;

            // Update heights
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private q1_AVLNode LeftRotate(q1_AVLNode x)
        {
            q1_AVLNode y = x.Right;
            q1_AVLNode T2 = y?.Left;

            //rotation
            y.Left = x;
            x.Right = T2;

            // parents
            y.Parent = x.Parent;
            x.Parent = y;
            if (T2 != null) T2.Parent = x;

            //Heights
            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }
    }
}
