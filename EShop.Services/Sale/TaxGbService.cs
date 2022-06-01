using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Sale
{
    public class TaxGbService : ITaxService
    {
        public decimal CalculateGrossAmount(ProductTypes productType, decimal netValue)
        {
            decimal tax = GetTax(productType) / 100m;
            return netValue + (netValue * tax);
        }

        public decimal GetTax(ProductTypes productType)
        {
            decimal result = 0;

            switch (productType)
            {
                case ProductTypes.Other:
                    result = 20;
                    break;
                case ProductTypes.Food:
                    result = 12.5m;
                    break;
                case ProductTypes.Clothes:
                    result = 20;
                    break;
                case ProductTypes.Books:
                    result = 0;
                    break;
                case ProductTypes.MedicalSupplies:
                    result = 0;
                    break;
                default:
                    result = 20;
                    break;
            }

            return result;
        }
    }
}
