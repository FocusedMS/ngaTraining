using System;

namespace ExceptionhandlingDay3
{
    // Main program class
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter Age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                CheckVotingEligibility(age); // method inside same class
            }
            catch (VotingException ex)
            {
                Console.WriteLine("Voting Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }
        }

        // Method to check eligibility
        static void CheckVotingEligibility(int age)
        {
            if (age < 18)
            {
                throw new VotingException("You are not eligible to vote.");
            }

            Console.WriteLine("You are eligible to vote.");
        }
    }

    // Custom exception class (placed after the Program class)
    internal class VotingException : Exception
    {
        public VotingException(string message) : base(message)
        {
        }
    }
}
