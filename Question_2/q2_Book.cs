using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_2
{
    internal class q2_Book
    {
        public q2_Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

    }
}
