using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public class PDFDocument : IDocument
    {
        public void Print()
        {
            Console.WriteLine("Printing PDF Document...");
        }
    }
}
