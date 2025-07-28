using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDay1
{
    internal class Calculation
    {
        public int sum(int a, int b)
        {
            return a + b;
        }
        public int sub(int a, int b)
        {
            return a - b;
        }
        public int mul(int a, int b)
        {
            return a * b;
        }

        public int div(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Div by zero is not allowed.");
            }
            return a / b;
        }

        static void Main()
        {
            int a, b;
            Console.WriteLine("Enter two numbers:");
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            Calculation calc = new Calculation();
            int result = calc.sum(a, b);
            Console.WriteLine("Sum: " + result);
            result = calc.sub(a, b);
            Console.WriteLine("Subtraction: " + result);
            result = calc.mul(a, b);
            Console.WriteLine("Multiplication: " + result);

        }
    }
}
