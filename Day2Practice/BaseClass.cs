using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    // Constructor Inheritance
    class BaseClass
    {
        public BaseClass()
        {
            Console.WriteLine("Base class constructor..");
        }
    }

    class DerivedClass : BaseClass
    {
        public DerivedClass()
        {
            Console.WriteLine("Derived class constructor...");
        }
    }

    internal class ConstructorInheritance
    {
        public static void Main(String[] args)
        {
            DerivedClass derivedClass = new DerivedClass();
            // Output:
            // Base class constructor..
            // Derived class constructor...
        }
    }
}
