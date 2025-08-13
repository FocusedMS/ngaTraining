using SOLIDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Services
{
    public class ReportGenerator : IReportGenerator
    {
        public string Generate()
        {
            return "Generated Report Content";
        }
    }
}

