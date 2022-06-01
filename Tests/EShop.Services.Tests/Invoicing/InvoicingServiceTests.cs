using EShop.Services.Invoicing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Tests.Invoicing
{
    public class InvoicingServiceTests
    {
        IInvoicingService invoicingService;

        [SetUp]
        public void Setup()
        {
            invoicingService = new InvoicingService();
        }

        [Test]
        public void GetCurrentMothInvoicesShouldReturnOnlyCurrentMonthInvoices()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Act 
            var actual = invoicingService.GetCurrentMonthInvoices();

            //Assert
            Assert.IsTrue(actual.All(x => x.InvoiceDate.Month == currentDateTime.Month && x.InvoiceDate.Year == currentDateTime.Year));
        }

        [Test]
        public void GetCurrentMothInvoicesShouldReturnExaclyOneEqualTo10()
        {
            var actual = invoicingService.GetCurrentMonthInvoices();

            Assert.That(actual, Has.Exactly(1).EqualTo(10));
        }
    }
}
