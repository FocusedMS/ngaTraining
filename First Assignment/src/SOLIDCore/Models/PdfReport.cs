using SOLIDCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Models
{
    public class PdfReport : Report
    {
        public override string GetContent()
        {
            return "PDF Report Content";
        }
    }
}

