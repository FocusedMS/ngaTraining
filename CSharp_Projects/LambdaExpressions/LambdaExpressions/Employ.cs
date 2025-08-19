using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressions
{
    internal class Employ : IComparable<Employ>
    {
        public int EmployId { get; set; }
        public string Name { get; set; }
        public double Basic { get; set; }
        public int CompareTo(Employ employ)
        {
            if (EmployId >= employ.EmployId)
            {
                return 1;
            }
            return -1;
        }

        public override string ToString()
        {
            return "Employ Id " + EmployId + " Employ Name " + Name + " Basic " + Basic;
        }
    }
}
