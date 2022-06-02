using EShop.Services.Invoicing;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Tests.Invoicing
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        ICalculatorService calculatorService;
        [SetUp]
        public void Setup()
        {
            calculatorService = new CalculatorService();
        }

        [Test]
        public void GetGrossFromNetShouldReturnValidGrossValue()
        {
            //Arrange 
            decimal netValue = 10m;
            decimal tax = 0.23m;
            decimal expected = 12.3m;

            //Act
            decimal actual = calculatorService.GetGrossFromNet(netValue, tax);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(10, 0.23, 12.3)]
        [TestCase(20, 0.23, 24.6)]
        [TestCase(10, 0.8, 18)]
        [TestCase(10, 1, 20)]
        public void GetGrossFromNetShouldReturnValidGrossValue2(decimal netValue, decimal tax, decimal expected)
        {
            //Act
            decimal actual = calculatorService.GetGrossFromNet(netValue, tax);

            //Assert
            Assert.AreEqual(expected, actual);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test, Sequential]
        public void GetGrossFromNetShouldReturnValidGrossValue3(
            [Values(10,20)]     decimal netValue, 
            [Values(0.23, 0.8)] decimal tax, 
            [Values(12.3, 36)]  decimal expected)
        {
            //Act
            decimal actual = calculatorService.GetGrossFromNet(netValue, tax);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetGrossFromNetShouldThrowsArgumentExceptionWhenTaxisLessThanZero()
        {
            //Arrange 
            decimal netValue = 10m;
            decimal tax = -1m;

            //Assert
            //Assert.Throws<ArgumentException>(() => calculatorService.GetGrossFromNet(netValue, tax));
            calculatorService.Invoking(cs => calculatorService.GetGrossFromNet(netValue, tax))
                .Should().Throw<ArgumentException>().WithMessage("Tax cannot be less than 0.");
        }

        [TearDown]
        public void TearDown()
        {
            calculatorService = null;
        }
    }
}
