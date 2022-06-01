using EShop.Data.Entities;

namespace EShop.Services.Sale
{
    public interface ITaxService
    {
        decimal GetTax(ProductTypes productType);
        decimal CalculateGrossAmount(ProductTypes productType, decimal netValue);
    }
}
