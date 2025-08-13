using SOLIDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Utilities
{
    public class ReportFormatter
    {
        private readonly IReportFormatter _formatter;

        public ReportFormatter(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public string GetFormattedReport(string data)
        {
            return _formatter.Format(data);
        }
    }
}

