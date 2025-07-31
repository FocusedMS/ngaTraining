using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSystem
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }

        public void Borrow()
        {
            IsBorrowed = true;
        }

        public void Return()
        {
            IsBorrowed = false;
        }
    }
}
