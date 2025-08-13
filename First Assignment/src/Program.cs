using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using SOLIDCore.Services;
using SOLIDCore.Formatters;
using SOLIDCore.Utilities;

namespace SOLIDApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use SRP classes
            var generator = new ReportGenerator();
            var saver = new ReportSaver();

            // Use OCP strategy pattern
            var formatter = new PdfFormatter(); // You can switch to ExcelFormatter too

            // Generate content
            string reportData = generator.Generate();

            // Format the report
            var reportFormatter = new ReportFormatter(formatter);
            string formattedData = reportFormatter.GetFormattedReport(reportData);

            // Save the report
            saver.Save(formattedData);

            Console.WriteLine("Report generation complete.");
            Console.ReadLine();
        }
    }
}

