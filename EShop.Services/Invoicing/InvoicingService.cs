using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Invoicing
{
    public interface IInvoicingService
    {
        IEnumerable<Invoice> GetCurrentMonthInvoices();
    }

    public class InvoicingService : IInvoicingService
    {
        public InvoicingService()
        {

        }

        public IEnumerable<Invoice> GetCurrentMonthInvoices()
        {
            return new List<Invoice>()
            {
                new Invoice()
                {
                    Id = 1,
                    InvoiceDate = DateTime.Now.Date
                },
            };
        }
    }    
}
