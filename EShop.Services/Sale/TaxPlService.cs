using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Sale
{
    public class TaxPlService : ITaxService
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
                    result = 23;
                    break;
                case ProductTypes.Food:
                    result = 5;
                    break;
                case ProductTypes.Clothes:
                    result = 23;
                    break;
                case ProductTypes.Books:
                    result = 5;
                    break;
                case ProductTypes.MedicalSupplies:
                    result = 8;
                    break;
                default:
                    result = 23;
                    break;
            }

            return result;
        }
    }
}
