using SOLIDCore.Interfaces;
using SOLIDCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Services
{
    public class ReportService
    {
        private readonly IReportGenerator _generator;
        private readonly IReportSaver _saver;

        public ReportService(IReportGenerator generator, IReportSaver saver)
        {
            _generator = generator;
            _saver = saver;
        }

        public void GenerateAndSaveReport()
        {
            var report = _generator.Generate();
            _saver.Save(report);
        }
    }
}


