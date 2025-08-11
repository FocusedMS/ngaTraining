using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDay4
{
    internal class EmployExample2
    {
        static void Main(string[] args)
        {
            Employ employ1 = new Employ();
            employ1.EmployeeId = 101;
            employ1.Name = "John Doe";
            employ1.Department = "HR";

            Employ employ2 = new Employ();
            employ2.EmployeeId = 102;
            employ2.Name = "Jane Smith";
            employ2.Department = "IT";

            Employ employ3 = new Employ();
            employ3.EmployeeId = 103;
            employ3.Name = "Alice Johnson";
            employ3.Department = "Finance";

            // Create a list to store Employ objects
            ArrayList employList = new ArrayList();
            employList.Add(employ1);
            employList.Add(employ2);
            employList.Add(employ3);


            foreach (Employ employ in employList)
            {
                Employ emp = (Employ)employ;
                Console.WriteLine(emp);
            }
        }
    }
}
