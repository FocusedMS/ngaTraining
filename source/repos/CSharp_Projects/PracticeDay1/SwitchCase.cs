using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDay1
{
    internal class SwitchCase
    {
        public void Check(char ch)
        {
            switch (ch)
            {
                case 'a':
                case 'A':
                case '1':
                    Console.WriteLine("Hi this is Arya Stark");
                    break;
                case 'b':
                case 'B':
                case '2':
                    Console.WriteLine("Hi This is madhu");
                    break;
                case 'c':
                case 'C':
                case '3':
                    Console.WriteLine("Hi this is Sansa Stark");
                    break;
                case 'd':
                case 'D':
                case '4':
                    Console.WriteLine("Hi This is Pravals");
                    break;
            }
        }
        static void Main()
        {
            char ch;
            Console.WriteLine("Enter a character:");
            ch = Convert.ToChar(Console.ReadLine());
            SwitchCase sc = new SwitchCase();
            sc.Check(ch);

        }
    }
}
