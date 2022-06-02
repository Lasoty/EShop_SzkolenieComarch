using EShop.Data.Entities;
using EShop.Data.Repositories;
using EShop.Services.Sale;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Tests.Invoicing
{
    [TestFixture(Category = "Developer")]
    public class SaleServiceTests
    {
        ISaleService saleService;
        Mock<IDiscountService> discountServiceMock;

        IEnumerable<Product> products = new List<Product>()
        {
            new Product {Id = 1, Name = "Test 1", EAN = "1234567891234", PriceNet = 10, Type = ProductTypes.Other },
            new Product {Id = 2, Name = "Test 2", EAN = "2345678912345", PriceNet = 20, Type = ProductTypes.Books },
        };

        Customer customer = new Customer()
        {
            Id = 1,
            Name = "Tester Testowy",
            Region = Regions.Poland
        };

        IList<Order> orders = new List<Order>();
        
        [SetUp]
        public void Setup()
        {
            Mock<IRepository<Order>> productRepositoryMock = new Mock<IRepository<Order>>();
            productRepositoryMock.Setup(p => p.GetAll()).Returns(() => orders.AsQueryable());
            productRepositoryMock.Setup(p => p.Add(It.IsAny<Order>())).Callback((Order o) => 
            {
                orders.Add(o);
            });


            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            ITaxService taxService = taxServiceMock.Object;

            taxServiceMock.Setup(x => x.GetTax(It.IsAny<ProductTypes>()))
                .Returns(1).Verifiable();

            discountServiceMock = new Mock<IDiscountService>();
            IDiscountService discountService = discountServiceMock.Object;
            discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>())).Returns(1).Verifiable();

            saleService = new SaleService(productRepositoryMock.Object, taxService, discountService);
        }

        [Test]
        public void CreateOrderShouldReturnsNotNullObject()
        {
            // Act
            var actual = saleService.CreateOrder(customer, products);

            // Assert
            actual.Should().NotBeNull();
        }

        [Test]
        public void CreateOrderShouldReturnOrderWithNotEmptyProducts()
        {
            //act
            var actual = saleService.CreateOrder(customer, products);

            //Assert

            actual.Products.Should().NotBeNull().And.NotBeEmpty()
                .And.NotContainNulls(x => x);
        }

        [Test]
        public void CreateOrderShouldUseCalculateDiscountOnluOnce()
        {
            var actual = saleService.CreateOrder(customer, products);
            discountServiceMock.Verify(x => x.CalculateDiscount(It.IsAny<decimal>()), Times.Once());
            Assert.Pass();
        }

        [Test]
        public void CreateOrderShouldTrySaveOrderEntity()
        {
            var order = saleService.CreateOrder(customer, products);

            orders.Should().Contain(order);
        }

        [Test]
        public void CreateOrderShouldHaveExcactlyCorrectProducts()
        {
            var actual = saleService.CreateOrder(customer, products);

            actual.Products.Should().SatisfyRespectively(
            first =>
            {
                first.Id.Should().Be(1);
                first.Name.Should().StartWith("Test");
                first.PriceNet.Should().Be(10);
                first.Type.Should().Be(ProductTypes.Other);
            },
            second =>
            {
                second.Id.Should().Be(2);
                second.Name.Should().EndWith("2");
                second.PriceNet.Should().Be(20);
                second.Type.Should().Be(ProductTypes.Books);
            });
        }

        [Test()]
        public void CreateOrderShouldReturnCorrectProducts()
        {
            //var actual = saleService.CreateOrder(customer, products);

            //actual.Products.Should().AllSatisfy(o =>
            //{
            //    o.Id.Should().BePositive();
            //    o.Name.Should().NotBeNullOrEmpty().And.StartWith("Test");
            //    o.EAN.Should().HaveLength(13);
            //});

            Assert.Ignore();
        }
    }
}
