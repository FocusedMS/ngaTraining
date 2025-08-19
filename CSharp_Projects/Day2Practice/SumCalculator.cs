using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    internal class SumCalculator
    {
        // No argument version
        public int CalculateSum()
        {
            return 5;
        }

        // One-argument version
        public int CalculateSum(int number)
        {
            return number + 5;
        }

        // Two-argument version
        public int CalculateSum(int number, int number2)
        {
            return number + number2;
        }

        public static void Main(string[] args)
        {
            SumCalculator calculator = new SumCalculator();

            Console.WriteLine(calculator.CalculateSum());
            Console.WriteLine(calculator.CalculateSum(23));
            Console.WriteLine(calculator.CalculateSum(23, 7));
        }
    }
}
