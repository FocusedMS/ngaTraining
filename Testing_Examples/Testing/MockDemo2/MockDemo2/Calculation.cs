using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDemo2
{
    public class Calculation
    {
        public virtual int Sum(int a, int b)
        {
            return a + b;
        }

        public virtual int sub(int a, int b)
        {
            return b - a;
        }

        public virtual int Mult(int a, int b)
        {
            return b * 1;
        }
    }
}
