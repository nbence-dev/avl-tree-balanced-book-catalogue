using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_2
{
    // Currently a BST Node
    internal class q1_AVLNode
    {
        // https://www.youtube.com/watch?v=lxHF-mVdwK8
        public q1_AVLNode(q1_Book book)
        {
            Book = book;
            Left = null;
            Right = null;
            Parent = null;
            Height = 1;

        }

        public q1_Book Book { get; set; }
        public q1_AVLNode Left { get; set; }
        public q1_AVLNode Right
        {
            get; set;
        }
        public q1_AVLNode Parent { get; set; }
        public int Height { get; set; }
    }
}
