using System;

namespace REDACTED_PROJECT_NAME
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first num:");
            int number1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the second num:");
            int number2 = Convert.ToInt32(Console.ReadLine());

            int sum = number1 + number2;
            Console.WriteLine("The Sum is: " + sum);

        }
    }
}