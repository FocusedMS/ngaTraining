using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockRepeat
{
    [TestFixture]
    public class WiproMockTest
    {
        public void TestMilestone1()
        {
            Mock<IWiproData> mock = new Mock<IWiproData>();
            mock.Setup(x => x.MilestoneExam1()).Returns("Milestone Exam 1 on Aug 1...");
            Assert.AreEqual("Milestone Exam 1 on Aug 1...", mock.Object.MilestoneExam1());
        }

        [Test]
        public void TestMilestone2()
        {
             Mock<IWiproData> mock = new Mock<IWiproData>();
            mock.Setup(x => x.MilestoneExam2()).Returns("Milestone Exam 2 on Aug 10...");
            Assert.AreEqual("Milestone Exam 2 on Aug 10...", mock.Object.MilestoneExam2());
        }
    }
}
