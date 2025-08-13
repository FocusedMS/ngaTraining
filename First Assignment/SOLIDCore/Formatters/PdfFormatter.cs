using SOLIDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Formatters
{
    public class PdfFormatter : IReportFormatter
    {
        public string Format(string data)
        {
            return $"PDF Format: {data}";
        }
    }
}

