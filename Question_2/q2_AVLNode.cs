using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_2
{
    // Currently a BST Node
    internal class q2_AVLNode
    {
        // https://www.youtube.com/watch?v=lxHF-mVdwK8
        public q2_AVLNode(q2_Book book)
        {
            Book = book;
            Left = null;
            Right = null;
            Parent = null;
            Height = 1;

        }

        public q2_Book Book { get; set; }
        public q2_AVLNode Left { get; set; }
        public q2_AVLNode Right
        {
            get; set;
        }
        public q2_AVLNode Parent { get; set; }
        public int Height { get; set; }
    }
}
