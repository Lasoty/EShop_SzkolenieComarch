using EShop.Services.Invoicing;
using FluentAssertions;
using FluentAssertions.Events;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Tests.Invoicing
{
    public class InvoiceFactoryTests
    {

        [Test]
        public void CreateIvoiceWhenInvoiceCreatedShouldRaiseInvoiceCreatedEvent()
        {
            InvoiceFactory invoiceFactory = new InvoiceFactory();
            IMonitor<InvoiceFactory> invoiceFactoryMonitor = invoiceFactory.Monitor();

            var invoice = invoiceFactory.CreateIvoice();

            invoiceFactoryMonitor.Should().Raise(nameof(InvoiceFactory.InvoiceCreated));
        }
    }
}
