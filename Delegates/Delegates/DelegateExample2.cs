﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class DelegateExample2
    {
        public delegate void MyDelegate(string s);

        public static void Show(string s)
        {
            Console.WriteLine("Welcome to Delegates, " + s + "...");
        }

        static void Main(string[] args)
        {
            MyDelegate obj = new MyDelegate(Show);
            obj("Chinnu");
        }
    }
}
