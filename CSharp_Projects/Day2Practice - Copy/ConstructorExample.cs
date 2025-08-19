using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    internal class ConstructorExample
    {
        // static constructor: runs only once, before the first object is created
        static ConstructorExample()
        {
            Console.WriteLine(" Static method called.");
        }

        // instance constructor: runs every time an object is created
        public ConstructorExample()
        {
            Console.WriteLine("Instance constructor called.");
        }

        public static void Main(String[] args)
        {
            ConstructorExample constructorExample = new ConstructorExample();
        }
    }
}
