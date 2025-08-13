using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using SOLIDCore.Interfaces;
using SOLIDCore.Services;

namespace SOLIDTests
{
    [TestFixture]
    public class ReportServiceTests
    {
        [Test]
        public void GenerateAndSaveReport_ShouldCallGenerateAndSaveOnce()
        {
            // Arrange: create mock dependencies
            var mockGenerator = new Mock<IReportGenerator>();
            var mockSaver = new Mock<IReportSaver>();

            mockGenerator.Setup(g => g.Generate()).Returns("Mock Report");

            var reportService = new ReportService(mockGenerator.Object, mockSaver.Object);

            // Act
            reportService.GenerateAndSaveReport();

            // Assert: check if Generate and Save were called exactly once
            mockGenerator.Verify(g => g.Generate(), Times.Once);
            mockSaver.Verify(s => s.Save("Mock Report"), Times.Once);
        }
    }
}
