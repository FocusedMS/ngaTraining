using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlingDay4
{
    internal class ReadEmployFile
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"C:\Files\Employ.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            Employ employ = (Employ)formatter.Deserialize(fs);
            Console.WriteLine(employ);
        }
    }
}
