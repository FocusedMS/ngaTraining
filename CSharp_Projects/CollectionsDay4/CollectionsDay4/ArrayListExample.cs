using System;
using System.Collections; // Needed for ArrayList

namespace CollectionsDay4
{
    internal class ArrayListExample
    {
        static void Main(string[] args)
        {
            // Create an ArrayList to store names
            ArrayList namesList = new ArrayList();

            // Add elements
            namesList.Add("Arya");
            namesList.Add("Sansa");
            namesList.Add("Jon");
            namesList.Add("Bran");
            namesList.Add("Rickon");
            namesList.Add("Robb");

            Console.WriteLine("Names in the list:");
            foreach (object name in namesList)
            {
                Console.WriteLine(name);
            }

            // Insert "Ned" at index 3
            namesList.Insert(3, "Ned");
            Console.WriteLine("\nAfter inserting Ned at index 3:");
            foreach (object name in namesList)
            {
                Console.WriteLine(name);
            }

            // Remove "Rickon"
            namesList.Remove("Rickon");
            Console.WriteLine("\nAfter removing Rickon:");
            foreach (object name in namesList)
            {
                Console.WriteLine(name);
            }

            // Remove item at index 2
            namesList.RemoveAt(2);
            Console.WriteLine("\nAfter removing the element at index 2:");
            foreach (object name in namesList)
            {
                Console.WriteLine(name);
            }
        }
    }
}
