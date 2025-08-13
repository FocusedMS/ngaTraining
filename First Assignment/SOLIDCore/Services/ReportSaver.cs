using SOLIDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Services
{
    public class ReportSaver : IReportSaver
    {
        public void Save(string data)
        {
            Console.WriteLine("Saving report: " + data);
        }
    }
}
