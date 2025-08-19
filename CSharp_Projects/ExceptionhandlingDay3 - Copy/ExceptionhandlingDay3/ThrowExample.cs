using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionhandlingDay3
{
    internal class ThrowExample
    {
        public void Show(int n)
        {
            if (n < 0)
            {
                throw new DivideByZeroException("Negative Numbers Not Allowed");
            }
            else if (n == 0)
            {
                throw new IndexOutOfRangeException("Zero is Invalid value...");
            }
            else
            {
                Console.WriteLine("N value is : " + n);
            }
        }
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Enter a number:");
            n = Convert.ToInt32(Console.ReadLine());
            ThrowExample throwExample = new ThrowExample();

            try
            {
                throwExample.Show(n);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Program from ThrowExample completed.");
            }

        }
    }
}
