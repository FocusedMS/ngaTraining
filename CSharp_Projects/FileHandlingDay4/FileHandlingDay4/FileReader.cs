using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingDay4
{
    internal class FileReader
    {
        static void Main(string[] args)
        {
            //Define the path to the file to be read
            string filePath = @"C:\Files\example.txt.txt";

            // create a filestream to open the file in readmode
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // create a StreamReader to read the file
            StreamReader streamReader = new StreamReader(fileStream);

            // Move the file pointer to the beginning of the file
            streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            string currentLine;

            Console.WriteLine("Reading contents of the file:\n");

            while ((currentLine = streamReader.ReadLine()) != null)
            {
                Console.WriteLine(currentLine);
            }

            streamReader.Close();
            fileStream.Close();
        }
    }
}
