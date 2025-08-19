using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionhandlingDay3
{
    internal class Division
    {
        static void Main(string[] args)
        {
            int a, b, c;
            Console.WriteLine("Enter 2 numbers for division:");
            try
            {
                a = Convert.ToInt32(Console.ReadLine());
                b = Convert.ToInt32(Console.ReadLine());
                c = a / b;
                Console.WriteLine("Division " + c);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Overflow Exception: " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("String Cannot be Converted as Integer" + ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Division by zero Impossible.." + ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Program from Division completed.");
            }
        }
    }
}
