using System;
using System.Collections.Generic;

namespace EShop.Data.Entities
{
    public class Order : BaseEntity
    {
        public Customer Buyer { get; set; }

        public ICollection<Product> Products { get; set; }

        public OrderStates State { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalNetAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalGrossAmount { get; set; }

    }

    public enum OrderStates
    {
        New,
        WaitingForPayment,
        InProgress,
        Finished
    }
}
