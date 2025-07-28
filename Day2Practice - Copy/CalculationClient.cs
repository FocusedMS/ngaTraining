using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    internal class CalculationClient
    {
        public int Sum(int firstNum, int secondNum)
        {
            return firstNum + secondNum;
        }

        public int Sub(int firstNum, int secondNum)
        {
            return firstNum - secondNum;
        }

        public int Mult(int firstNum, int secondNum)
        {
            return firstNum * secondNum;
        }
        static void Main(string[] args)
        {
            int firstNum, secondNum;

            Console.WriteLine("Enter 2 Numbers:");
            firstNum = Convert.ToInt32(Console.ReadLine());
            secondNum = Convert.ToInt32(Console.ReadLine());


            CalculationClient client = new CalculationClient();

            Console.WriteLine("Sum is " + client.Sum(firstNum, secondNum));
            Console.WriteLine("Sub is " + client.Sub(firstNum, secondNum));
            Console.WriteLine("Mult is " + client.Mult(firstNum, secondNum));
        }
    }
}
