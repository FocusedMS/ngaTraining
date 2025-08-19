using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDay4
{
    internal class Employ
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }


        public override string ToString()
        {
            return "Employ No " + EmployeeId + ", Name: " + Name + ", Department: " + Department;
        }
    }
}
