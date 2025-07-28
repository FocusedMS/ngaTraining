using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionhandlingDay3
{
    internal class ArrayExample
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[5];

            try
            {
                numbers[10] = 323;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Index out of range: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
