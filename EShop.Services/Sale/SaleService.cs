using EShop.Data.Entities;
using EShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Sale
{
    public class SaleService : ISaleService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly ITaxService taxService;
        private readonly IDiscountService discountService;

        public SaleService(
            IRepository<Order> orderRepository,
            ITaxService taxService,
            IDiscountService discountService
            )
        {
            this.orderRepository = orderRepository;
            this.taxService = taxService;
            this.discountService = discountService;
        }

        public Order CreateOrder(Customer buyer, IEnumerable<Product> products)
        {
            decimal netAmount = products.Sum(x => x.PriceNet);
            decimal discount = discountService.CalculateDiscount(netAmount);
            Order order = new()
            { 
                Buyer = buyer,
                Products = products.ToList(),
                OrderDate = DateTime.Now,
                State = OrderStates.New,
                TotalNetAmount = products.Sum(x => x.PriceNet),
                Discount = discountService.CalculateDiscount(netAmount),
                TotalGrossAmount = CalculateTotalGrossAmount(products)
            };
            
            return order;
        }

        private decimal CalculateTotalGrossAmount(IEnumerable<Product> products)
        {
            decimal result = 0;

            foreach (var item in products)
            {
                result += taxService.CalculateGrossAmount(item.Type, item.PriceNet);
            }

            return result;
        }
    }
}
