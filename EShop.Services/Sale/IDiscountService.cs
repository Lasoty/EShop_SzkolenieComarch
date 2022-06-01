using EShop.Data.Entities;
using System.Collections.Generic;

namespace EShop.Services.Sale
{
    public interface IDiscountService
    {
        public decimal CalculateDiscount(decimal summaryPrice);
        public decimal CalculateDiscount(IEnumerable<Product> orderedProducts);
    }
}
