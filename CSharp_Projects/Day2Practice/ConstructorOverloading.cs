using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Practice
{
    internal class ConstructorOverloading
    {
        int firstNumber, secondNumber;

        // Default constructor
        public ConstructorOverloading()
        {
            firstNumber = 5;
            secondNumber = 7;
        }

        // Parameterized constructor
        public ConstructorOverloading(int first, int second)
        {
            this.firstNumber = first;
            this.secondNumber = second;
        }

        // Method to display the numbers
        public void DisplayNumbers()
        {
            Console.WriteLine("First Number: " + firstNumber + ", Second Number: " + secondNumber);
        }

        public static void Main(String[] args)
        {
            ConstructorOverloading constructorOverloading = new ConstructorOverloading();
        }
    }
}
