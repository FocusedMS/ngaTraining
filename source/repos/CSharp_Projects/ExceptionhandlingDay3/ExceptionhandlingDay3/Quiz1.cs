using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionhandlingDay3
{
    internal class Quiz1
    {
        static void Main(string[] args)
        {
            int[] a = new int[] { 1, 2 };
            int x = 5, y = 0;
            try
            {
                a[2] = x / y;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Division " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
