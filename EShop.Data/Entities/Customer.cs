namespace EShop.Data.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public Regions Region { get; set; }
    }

    public enum Regions
    {
        GreatBritain,
        Poland
    }
}