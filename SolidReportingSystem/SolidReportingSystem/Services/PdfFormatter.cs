using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidReportingSystem.Interfaces;

namespace SolidReportingSystem.Services
{
    public class PdfFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"PDF FORMAT: {content}";
        }
    }
}
