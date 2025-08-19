using ExtensionLibrary;       // Access to Operations
using ExtensionExample2;
using System;

namespace ExtensionExample2
{
    internal class  Program
    {
        static void Main(string[] args)
        {
            Operations operations = new Operations();
            Console.WriteLine(operations.MileStone1());
            Console.WriteLine(operations.MileStone2());
            Console.WriteLine(operations.Milestone3());
            Console.WriteLine(operations.Milestone4());
            Console.WriteLine(operations.Project());
        }
    }
}