using EShop.Data.Entities;
using System.Collections.Generic;
using System.Globalization;

namespace EShop.Services.Sale
{
    public interface ISaleService
    {
        Order CreateOrder(Customer buyer, IEnumerable<Product> products);
    }
}
