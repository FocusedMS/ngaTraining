using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingDay4
{
    [Serializable]
    internal class Employ
    {
        public int EmployId { get; set; }
        public string Name { get; set; }
        public double Basic { get; set; }

        public override string ToString()
        {
            return "Employ ID: " + EmployId + ", Name: " + Name + ", Basic: " + Basic;
        }
    }
}
