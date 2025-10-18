using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_1
{
    // Currently a BST Node
    internal class q1_AVLNode
    {
        public q1_AVLNode(q1_Book book)
        {
            Book = book;
        }

        public q1_Book Book { get; set; }
        public q1_AVLNode Left { get; set; }
        public q1_AVLNode Right
        {
            get; set;
        }
    }
}
