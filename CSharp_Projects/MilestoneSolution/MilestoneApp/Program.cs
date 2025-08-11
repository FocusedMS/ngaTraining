using System;
using MilestoneLibrary;
using MilestoneApp;

namespace MilestoneApp
{
    internal class  Program
    {
        static void Main(string[] args)
        {
            Operations operations = new Operations();

            Console.WriteLine(operations.Milestone1());
            Console.WriteLine(operations.Milestone2());
            Console.WriteLine(operations.Milestone3());
            Console.WriteLine(operations.Milestone4());
            Console.WriteLine(operations.FinalProject());
        }
    }
}