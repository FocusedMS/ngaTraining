using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDay1
{
    internal class CircleProg
    {
        public void Calc(double radius)
        {
            double area, circ;
            area = Math.PI * radius * radius;
            circ = 2 * Math.PI * radius;
            Console.WriteLine("Area of Circle: " + area);
            Console.WriteLine("Circumference of Circle: " + circ);
        }

        static void Main()
        {
            double radius;
            Console.WriteLine("Enter radius: ");
            radius = Convert.ToDouble(Console.ReadLine());
            CircleProg circle = new CircleProg();
            circle.Calc(radius);
        }
    }
}
