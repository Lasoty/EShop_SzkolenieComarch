namespace EShop.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string EAN { get; set; }

        public decimal PriceNet { get; set; }

        public ProductTypes Type { get; set; }

    }

    public enum ProductTypes
    {
        Other,
        Food,
        Clothes,
        Books,
        MedicalSupplies
    }
}
