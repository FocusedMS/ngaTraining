using SOLIDCore.Interfaces;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Clients
{
    public class BasicReport : IGeneratable, ISavable
    {
        public string Generate()
        {
            return "Basic Report Content";
        }

        public void Save(string data)
        {
            Console.WriteLine("Saving basic report: " + data);
        }
    }
}

