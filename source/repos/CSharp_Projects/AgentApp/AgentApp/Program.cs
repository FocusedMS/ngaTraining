using AgentApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new AgentService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Agent Management ---");
                Console.WriteLine("1. Add Agent");
                Console.WriteLine("2. View All Agents");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("City: ");
                        string city = Console.ReadLine();

                        Console.Write("Gender (MALE/FEMALE): ");
                        string gender = Console.ReadLine();

                        Console.Write("Premium Amount: ");
                        double.TryParse(Console.ReadLine(), out double premiumAmount);

                        string result = service.AddAgent(name, city, gender, premiumAmount);
                        Console.WriteLine(result);
                        break;

                    case "2":
                        foreach (var agent in service.GetAllAgents())
                            Console.WriteLine(agent);
                        break;

                    case "3":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
