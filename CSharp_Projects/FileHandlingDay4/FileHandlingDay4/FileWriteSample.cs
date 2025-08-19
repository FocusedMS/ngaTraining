using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingDay4
{
    internal class FileWriteSample
    {
        static void Main(string[] args)
        {

            // Create a filstream to write to a file t specified path
            FileStream fs = new FileStream(@"C:\Files\example.txt", FileMode.Create, FileAccess.Write);

            // Create a StreamWriter to write to the file
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("This is the first line of the file.");
            sw.WriteLine("Thankyou all");
            sw.WriteLine("Trainer : Mr. XYZ");

            // Flush the StreamWriter to ensure all data is written to the file

            sw.Flush();
            sw.Close();
            fs.Close();

            Console.WriteLine("File written successfully!");

        }
    }
}
