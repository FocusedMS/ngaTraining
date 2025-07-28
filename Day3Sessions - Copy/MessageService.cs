using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Sessions
{
    class MessageService
    {
        public void ShowInstanceMessage()
        {
            Console.WriteLine("This is an instance method of MessageService class.");
        }

        public static void ShowStaticmessage()
        {
            Console.WriteLine("This is a static method of MessageService class.");
        }
    }
    internal class StaticMethodDemo
    {
        static void Main(string[] args)
        {
            MessageService.ShowStaticmessage();
            new MessageService().ShowInstanceMessage();

        }
    }
}
