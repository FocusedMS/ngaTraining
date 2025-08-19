using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Sessions
{
    internal class StaticCounterDemo
    {
        static int sharedCounter;

        public void IncrementCounter()
        {
            sharedCounter++;
        }

        public void DisplayCounter()
        {
            Console.WriteLine("Current Count's value: " + sharedCounter);
        }


        static void Main(string[] args)
        {
            StaticCounterDemo firstInstance = new StaticCounterDemo();
            StaticCounterDemo secondInstance = new StaticCounterDemo();

            firstInstance.IncrementCounter();
            firstInstance.DisplayCounter();

            secondInstance.IncrementCounter();
            secondInstance.DisplayCounter();
        }
    }
}
