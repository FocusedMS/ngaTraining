using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Demo
    {
        public string[] names = new string[5];
        public string this[int i]
        {
            get
            {
                return names[i];
            }
            set
            {
                names[i] = value;
            }
        }

        public class IndexerExample
        {
            public static void Main(string[] args)
            {
                Demo demo = new Demo();
                demo[0] = "Alice";
                demo[1] = "Bob";
                demo[2] = "Charlie";
                demo[3] = "Diana";
                demo[4] = "Eve";

                Console.WriteLine("Names in the demo object:");
                foreach (var v in demo.names)
                {
                    Console.WriteLine(v);
                }
            }
        }
    }
}
