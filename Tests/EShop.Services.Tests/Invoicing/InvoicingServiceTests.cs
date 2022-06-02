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
            var firstDayOfMonth = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            //Act 
            var actual = invoicingService.GetCurrentMonthInvoices();

            //Assert
            //Assert.IsTrue(actual.All(x => x.InvoiceDate.Month == currentDateTime.Month && x.InvoiceDate.Year == currentDateTime.Year));

            actual.Should().AllSatisfy(x =>
            {
                x.Should().NotBeNull();
                x.InvoiceDate.Should().BeOnOrAfter(firstDayOfMonth).And.BeOnOrBefore(lastDayOfMonth);
            });
        }


    }
}
