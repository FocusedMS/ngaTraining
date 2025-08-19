using System;

namespace ExceptionFiltersDay4
{
    // Custom exception class
    public class NameLengthException : Exception
    {
        public NameLengthException(string message) : base(message) { }
    }

    internal class FilterExample1
    {
        static void EvaluateNameLength(string inputName)
        {
            string lengthCategory = "";

            if (inputName.Length >= 0 && inputName.Length <= 3)
            {
                lengthCategory = "short";
            }
            else if (inputName.Length > 3 && inputName.Length <= 10)
            {
                lengthCategory = "Medium";
            }
            else if (inputName.Length > 10)
            {
                lengthCategory = "Long";
            }

            // Throw exception based on category
            if (lengthCategory == "short")
            {
                throw new NameLengthException("Short name exception occurred.");
            }
            else if (lengthCategory == "Medium")
            {
                throw new NameLengthException("Medium name exception occurred.");
            }
            else if (lengthCategory == "Long")
            {
                throw new NameLengthException("Long name exception occurred.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string userInput = Console.ReadLine();

            try
            {
                EvaluateNameLength(userInput);
            }
            catch (NameLengthException ex) when (ex.Message.Contains("short"))
            {
                Console.WriteLine("Caught a short name exception: " + ex.Message);
            }
            catch (NameLengthException ex) when (ex.Message.Contains("Medium"))
            {
                Console.WriteLine("Caught a medium name exception: " + ex.Message);
            }
            catch (NameLengthException ex) when (ex.Message.Contains("Long"))
            {
                Console.WriteLine("Caught a long name exception: " + ex.Message);
            }
            catch (NameLengthException ex)
            {
                Console.WriteLine("Caught an unknown name length exception: " + ex.Message);
            }
        }
    }
}
