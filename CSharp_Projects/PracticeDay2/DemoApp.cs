using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PracticeDay2
{
    public class Quiz2
    {
        public void Increment(ref int x)
        {
            ++x;
        }
        public static void Main(string[] args)
        {
            int x = 12;
            Quiz2 demo = new Quiz2();
            demo.Increment(ref x);
            Console.WriteLine(x);
        }
    }
}
