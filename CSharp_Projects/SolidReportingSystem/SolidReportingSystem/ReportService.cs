using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidReportingSystem.Interfaces;

namespace SolidReportingSystem
{
    public class ReportService
    {
        private readonly IReportGenerator _generator;
        private readonly IReportFormatter _formatter;
        private readonly IReportSaver _saver;

        public ReportService(IReportGenerator generator, IReportFormatter formatter, IReportSaver saver)
        {
            _generator = generator;
            _formatter = formatter;
            _saver = saver;
        }

        public void GenerateAndSaveReport()
        {
            var content = _generator.GenerateReport();
            var formatted = _formatter.Format(content);
            _saver.Save(formatted);
        }
    }
}
