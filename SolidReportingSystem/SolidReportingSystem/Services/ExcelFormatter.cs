using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidReportingSystem.Interfaces;

namespace SolidReportingSystem.Services
{
    public class ExcelFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"EXCEL FORMAT: {content}";
        }
    }
}
