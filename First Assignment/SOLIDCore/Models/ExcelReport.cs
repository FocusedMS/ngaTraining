using SOLIDCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Models
{
    public class ExcelReport : Report
    {
        public override string GetContent()
        {
            return "Excel Report Content";
        }
    }
}

