using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class MultiCastDelegate5
    {
        public delegate void DotnetFsd();

        public static void Project()
        {
            Console.WriteLine("Capstone Project to be done Last...");
        }

        public static void Milestone1()
        {
            Console.WriteLine("Milestone 1 to be conducted on core topics");
        }

        public static void Milestone2()
        {
            Console.WriteLine("Milestone 2 to be conducted on dotnet core topics");
        }

        public static void Milestone3()
        {
            Console.WriteLine("Milestone 3 to be conducted on Asp.net topics");
        }

        public static void Milestone4()
        {
            Console.WriteLine("Milestone 2 to be conducted on react topics");
        }

        static void Main()
        {
            DotnetFsd obj = new DotnetFsd(Milestone1);
            obj += Milestone2;
            obj += Milestone3;
            obj += Milestone4;
            obj += Project;

            obj();

        }
    }
}
