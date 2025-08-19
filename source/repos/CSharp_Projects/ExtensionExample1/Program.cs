using System;

namespace ExtensionExample1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculation calc = new Calculation();
            MathOperations match = new MathOperations();

            int sumResult = calc.sum(5, 10);
            int subResult = calc.sub(10, 5);
            int addResult = match.Add(5, 10);
            int mulResult = match.Mul(5, 10);

            Console.WriteLine($"Calculation Sum: {sumResult}");
            Console.WriteLine($"Calculation Subtraction: {subResult}");
            Console.WriteLine($"MathOperations Add : {addResult}");
            Console.WriteLine($"MathOperations Multiply: {mulResult}");

        }
    }
}