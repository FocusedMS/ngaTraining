using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SolidReportingSystem.Interfaces;

namespace SolidReportingSystem.Services
{
    public class ReportGenerator : IReportGenerator
    {
        public string GenerateReport()
        {
            return "This is the original report content.";
        }
    }
}
