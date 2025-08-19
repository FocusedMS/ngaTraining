using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    class Parent
    {
        public void ShowMessage()
        {
            Console.WriteLine("This is a message from the parent class.");
        }
    }

    class Child : Parent
    {
        public void DisplayMessage()
        {
                   Console.WriteLine("This is a message from the child class.");
        }
    }

    internal class  InheritedExample
    {
        public static void Main(String[] args)
        {
            Child childObj = new Child();
            childObj.ShowMessage(); // Calling method from parent class
            childObj.DisplayMessage(); // Calling method from child class
        }
    }


}
