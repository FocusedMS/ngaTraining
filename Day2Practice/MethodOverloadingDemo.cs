using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    internal class MethodOverloadingDemo
    {
        public void Display(int number)
        {
            Console.WriteLine("Display method with int param: " + number);
        }

        public void Display(double number)
        {
            Console.WriteLine("Display method with double param: " + number);
        }

        public void Display(string text)
        {
            Console.WriteLine("Display method with string param: " + text);
        }

        public static void Main(string[] args)
        {
            MethodOverloadingDemo demo = new MethodOverloadingDemo();
            demo.Display(12);
            demo.Display(3.5);
            demo.Display("text");
        }
    }
}
