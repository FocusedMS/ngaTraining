using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary;

namespace MyLibrary.Tests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var calc = new Calculator();

            int result = calc.Add(10, 5);

            Assert.AreEqual(15, result);
        }
    }
}
