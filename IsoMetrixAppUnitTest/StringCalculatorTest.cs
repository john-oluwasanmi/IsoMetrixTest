using IsoMetrixTest;
using NUnit.Framework;
using System;

namespace IsoMetrixAppUnitTest
{
    [TestFixture]
    public class StringCalculatorTest
    {
        IStringCalculator stringCalculator;

        [SetUp]
        public void Setup()
        {
            stringCalculator = new StringCalculator();
        }

        [Test]
        public void Add_PositiveNumbersdWithNoDelimeter_ReturnSum()
        {
            var result = stringCalculator.Add("0,1,2,3");
            Assert.AreEqual(result, 6);

        }

        [Test]
        public void Add_PositiveNumbersdWithNewline_ReturnSum()
        {
            var result = stringCalculator.Add("1\n2,3");
            Assert.AreEqual(result, 6);
        }

        [Test]
        public void Add_PositiveNumberOneLength_ReturnSum()
        {
            var result = stringCalculator.Add("1,\n");
            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_PositiveNumbersIgnoreNumberMoreThan1000_ReturnSum()
        {
            var result = stringCalculator.Add("1\n2,3,1001");
            Assert.AreEqual(result, 6);
        }

        [Test]
        public void Add_PositiveNumberWithMultiCharacterDelimeterChanged_ReturnSum()
        {
            var result = stringCalculator.Add(@"//[***]\n1***2***3");
            Assert.AreEqual(result, 6);
        }


        [Test]
        public void Add_PositiveNumberWithSingleCharacterDelimeterChanged_ReturnSum()
        {
            var result = stringCalculator.Add(@"//;\n1;2");
            Assert.AreEqual(result, 3);
        }


        [Test]
        public void Add_PositiveNumberWithMultipleDelimeterChanged_ReturnSum()
        {
            var result = stringCalculator.Add(@"//[*][%]\n1*2%3");
            Assert.AreEqual(result, 6);
        }


        [Test]
        public void AddNegativeNumbersdWithNoDelimeter_ThrowException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => stringCalculator.Add("0,1,2,-33"));
            Assert.That(ex.Message, Is.EqualTo("negatives not allowed -33"));
        }
    }
}