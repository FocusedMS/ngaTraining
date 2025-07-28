using LambdaExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaDemos
{
    internal class LambdaExpr1
    {
        static void Main()
        {
            List<Employ> employList = new List<Employ>
            {
                new Employ{EmployId=1,Name="Sansa",Basic=88323},
                new Employ{EmployId=2,Name="Arya",Basic=91133},
                new Employ{EmployId=3,Name="Ned",Basic=80022},
                new Employ{EmployId=4,Name="Bran",Basic=90321},
                new Employ{EmployId=5,Name="Jon",Basic=78822},
                new Employ{EmployId=6,Name="Theon",Basic=78823},
            };

            Console.WriteLine("Employ List  ");
            var result1 = employList.Select(x => x);
            foreach (var v in result1)
            {
                Console.WriteLine(v);
            }

            var result2 = employList.Select(x => new { x.Name, x.Basic });
            Console.WriteLine("Projected Fields are");
            foreach (var v in result2)
            {
                Console.WriteLine(v);
            }

            var result3 = employList.Where(x => x.Basic >= 90000);
            Console.WriteLine("Salary > 90000 records are");
            foreach (var v in result3)
            {
                Console.WriteLine(v);
            }
            var result4 = employList.Where(x => x.Name.StartsWith("P"));
            foreach (var v in result4)
            {
                Console.WriteLine(v);
            }
        }
    }
}