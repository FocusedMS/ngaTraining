using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionhandlingDay3
{
    internal class StrExample
    {
        static void Main(string[] args)
        {
            string str = "Hello World";
            try
            {
                Console.WriteLine(str.Substring(2, 100));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Argument out of range: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Program from StrExample completed.");
            }
        }
    }
}
