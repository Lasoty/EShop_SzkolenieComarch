using EShop.Data.Entities;
using EShop.Services.Sale;
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
    public class TaxPlServiceTests
    {
        [TestCase(ProductTypes.Food, 5)]
        [TestCase(ProductTypes.Clothes, 23)]
        [TestCase(ProductTypes.Other, 8)]
        public void GetTaxForProductTypeShouldReturnCorrectTax(ProductTypes productTypes, decimal expected)
        {
            // Arrange 
            ITaxService taxService = new TaxPlService();

            //Act
            decimal actual = taxService.GetTax(productTypes);

            //Assert
            actual.Should().BeGreaterThanOrEqualTo(0)
                .And.Be(expected);
        }

        [TestCase(ProductTypes.Other, 8)]
        public void GetTaxForProductTypeShouldReturnCorrectTaxAssert(ProductTypes productTypes, decimal expected)
        {
            // Arrange 
            ITaxService taxService = new TaxPlService();

            //Act
            decimal actual = taxService.GetTax(productTypes);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
