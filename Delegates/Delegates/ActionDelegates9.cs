using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class ActionDelegates9
    {
        public static void Greeting(string s)
        {
            Console.WriteLine("Good Morning " + s);
        }

        public static void EndNote(string s)
        {
            Console.WriteLine("Good Night " + s);
        }

        static void Main(string[] args)
        {
            string str;
            Console.WriteLine("Enter Name ");
            str = Console.ReadLine();
            Action<string> obj = Greeting;
            obj += EndNote;
            obj(str);
        }
    }
}
