using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Sale
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscount(decimal summaryPrice)
        {
            if (summaryPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(summaryPrice), "Summary price cannot be less than 0.");

            decimal result = summaryPrice switch
            {
                <= 100 => 0,
                > 100 and <= 200 => 10,
                > 200 and <= 500 => 20,
                _ => 30,
            };
            return result;
        }

        public decimal CalculateDiscount(IEnumerable<Product> orderedProducts)
        {
            if (orderedProducts == null)
                throw new ArgumentNullException(nameof(orderedProducts), "Parameter cannot be null.");

            if (!orderedProducts.Any())
                throw new ArgumentOutOfRangeException(nameof(orderedProducts), "Ordered products must have at least one item.");


            return CalculateDiscount(orderedProducts.Sum(p => p.PriceNet));
        }
    }
}
