using NUnit.Framework;
using SolidReportingSystem;
using SolidReportingSystem.Services;
using SolidReportingSystem.Interfaces;
using System.IO;

namespace SolidReportingSystem.Tests
{
    public class ReportServiceTests
    {
        [Test]
        public void GenerateAndSaveReport_ShouldCreateFormattedFile()
        {
            IReportGenerator generator = new ReportGenerator();
            IReportFormatter formatter = new PdfFormatter();
            IReportSaver saver = new FileReportSaver();
            var service = new ReportService(generator, formatter, saver);

            service.GenerateAndSaveReport();

            string output = File.ReadAllText("ReportOutput.txt");
            Assert.That(output, Does.Contain("PDF FORMAT"));
        }
    }
}
