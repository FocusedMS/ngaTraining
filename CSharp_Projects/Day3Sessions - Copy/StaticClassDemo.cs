using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Sessions
{
    static class TrainingInfo
    {
        public static void DisplayMessage()
        {
            Console.WriteLine("Displaymessage() Welcome to the Static Class Demo!");
        }

        public static string GetTrainingDetails()
        {
            return "This is a static class that provides training information.";
        }
    }
    internal class StaticClassDemo
    {
        static void Main(string[] args)
        {
            TrainingInfo.DisplayMessage();
            Console.WriteLine(TrainingInfo.GetTrainingDetails());
        }
    }
}
