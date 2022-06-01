using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Invoicing
{
    public class InvoiceFactory
    {
        public InvoiceFactory()
        {

        }

        public Invoice CreateIvoice()
        {
            var result = new Invoice();
            InvoiceCreated?.Invoke(this, result);
            return new Invoice();
        }

        public event EventHandler<Invoice> InvoiceCreated;
    }
}
