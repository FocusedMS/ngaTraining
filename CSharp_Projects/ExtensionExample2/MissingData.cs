using ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionExample2
{
    public static class MissingData
    {
        public static string Milestone3(this Operations oper)
        {
            return "Milestone 3 on .Net core JWT WebApi";
        }

        public static string Milestone4(this Operations oper)
        {
            return "Milestone 4 on react and .net core Combination..";
        }

        public static string Project(this Operations oper)
        {
            return "Capstone Project to be Submitted Mandatorily"; 
        }
    }
}
