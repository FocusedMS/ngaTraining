using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidReportingSystem.Interfaces
{
    public interface IReportSaver
    {
        void Save(string formattedReport);
    }
}

