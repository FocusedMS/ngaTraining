using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Sessions
{
    internal class StaticExample
    {
        static int objectCounter;

        public void IncrementCounter()
        {
            objectCounter++;
        }
         
        public void DisplayCounter()
        {
            Console.WriteLine("Current Count: " + objectCounter);
        }

        static void Main(string[] args)
        {
            StaticExample instanceOne = new StaticExample();
            StaticExample instanceTwo = new StaticExample();
            StaticExample instanceThree = new StaticExample();

            instanceOne.IncrementCounter();
            instanceTwo.IncrementCounter();
            instanceThree.IncrementCounter();

            instanceOne.DisplayCounter();
        }
    }
}
