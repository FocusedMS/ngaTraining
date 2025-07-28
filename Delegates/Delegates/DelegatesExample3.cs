using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class DelegatesExample3
    {
        public delegate void MyDelegate(int n);

        public static void Fact(int n)
        {
            int fact = 1;
            for (int i = 1; i<= n ; i++)
            {
                fact = fact * i;
            }
            Console.WriteLine("Factorial of " + n + " is " + fact);
        }


        public static void PosNeg(int n)
        {
            if(n >= 0)
            {
                Console.WriteLine("" + n + " is a positive number.");
            }
            else
            {
                Console.WriteLine(n + " is a negative number.");
            }
        }

        public static void EvenOdd(int n)
        {
            if (n % 2 == 0)
            {
                Console.WriteLine(n + " is an even number.");
            }
            else
            {
                Console.WriteLine(n + " is an odd number.");
            }
        }

        static void Main()
        {
            int n;
            Console.WriteLine("Enter N value: ");
            n = Convert.ToInt32(Console.ReadLine());
            MyDelegate obj = new MyDelegate(PosNeg);
            obj += new MyDelegate(Fact);
            obj += new MyDelegate(EvenOdd);
            obj(n);
        }
    }
}
