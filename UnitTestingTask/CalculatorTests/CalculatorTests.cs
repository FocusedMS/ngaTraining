using NUnit.Framework;
using CalculatorLibrary;
using System;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void AddSum()
        {
            Assert.AreEqual(10, calculator.Add(6, 4));
        }

        [Test]
        public void AddWithZero()
        {
            Assert.AreEqual(5, calculator.Add(5, 0));
        }

        [Test]
        public void SubtractDiff()
        {
            Assert.AreEqual(2, calculator.Subtract(7, 5));
        }

        [Test]
        public void SubtractWithZero()
        {
            Assert.AreEqual(8, calculator.Subtract(8, 0));
        }

        [Test]
        public void MultiplyProd()
        {
            Assert.AreEqual(21, calculator.Multiply(3, 7));
        }

        [Test]
        public void MultWithZero()
        {
            Assert.AreEqual(0, calculator.Multiply(100, 0));
        }

        [Test]
        public void Div()
        {
            Assert.AreEqual(2, calculator.Divide(10, 5));
        }

        [Test]
        public void DivByZeroThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(9, 0));
        }

        
    }
}