using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidReportingSystem;
using SolidReportingSystem.Interfaces;
using SolidReportingSystem.Services;

class Program
{
    static void Main()
    {
        IReportGenerator generator = new ReportGenerator();
        IReportFormatter formatter = new PdfFormatter();
        IReportSaver saver = new FileReportSaver();

        var reportService = new ReportService(generator, formatter, saver);
        reportService.GenerateAndSaveReport();

        Console.WriteLine("Report generated and saved successfully");
    }
}

