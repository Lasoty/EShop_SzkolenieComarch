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
    public class DiscountServiceTests
    {
        IDiscountService discountService;

        [SetUp]
        public void Setup()
        {
            discountService = new DiscountService();
        }


        [TestCase(0, 0)]
        [TestCase(99, 0)]
        [TestCase(100, 0)]
        [TestCase(101, 10)]
        [TestCase(150, 10)]
        [TestCase(199, 10)]
        [TestCase(200, 10)]
        [TestCase(201, 20)]
        [TestCase(300, 20)]
        [TestCase(499, 20)]
        [TestCase(500, 20)]
        [TestCase(501, 30)]
        [TestCase(600, 30)]
        [TestCase(1000, 30)]
        [TestCase(10000, 30)]
        [TestCase(100000, 30)]
        public void CalculateDiscountForSummaryPriceShouldReturnCorrectValue(decimal summaryValue, decimal expected)
        {
            //act
            decimal actual = discountService.CalculateDiscount(summaryValue);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void CalculateDiscountForSummaryPriceShouldThrowsAnException(decimal summaryValue)
        {
            discountService.Invoking(ds => ds.CalculateDiscount(summaryValue))
                .Should().Throw<ArgumentOutOfRangeException>().WithMessage("Summary price cannot be less than 0.*");
        }

        private static readonly List<Product> emptyProductList = new();

        [TestCaseSource(nameof(emptyProductList))]
        public void CalculateDiscountForEmptyListShouldThrowsAnException(List<Product> emptyList)
        {
            //Assert
            discountService.Invoking(ds => ds.CalculateDiscount(emptyList))
                .Should().Throw<ArgumentOutOfRangeException>().WithMessage("Ordered products must have at least one item*");

        }

        [TearDown]
        public void TearDown()
        {
            discountService = null;
        }
    }
}
