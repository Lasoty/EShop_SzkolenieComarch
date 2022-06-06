using EShop.Data.Entities;
using EShop.Services.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EShop.Services.xTests.Sale
{
    public class TaxPlServiceTests : IDisposable
    {
        ITaxService taxService;

        public TaxPlServiceTests()
        {
            taxService = new TaxPlService();
        }

        public void Dispose()
        {
            taxService = null;
        }

        [Theory]
        [InlineData(ProductTypes.Food, 5)]
        [InlineData(ProductTypes.Clothes, 23)]
        [InlineData(ProductTypes.Other, 23)]
        public void GetTaxForProductTypeShouldReturnCorrectTax(ProductTypes productTypes, decimal expected)
        {
            // Act
            decimal actual = taxService.GetTax(productTypes);

            //Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object> emptyProductList = new List<Product>();

        
    }
}
