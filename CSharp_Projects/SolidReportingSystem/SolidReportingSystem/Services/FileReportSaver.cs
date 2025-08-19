using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidReportingSystem.Interfaces;

namespace SolidReportingSystem.Services
{
    public class FileReportSaver : IReportSaver
    {
        public void Save(string formattedReport)
        {
            File.WriteAllText("ReportOutput.txt", formattedReport);
        }
    }
}
