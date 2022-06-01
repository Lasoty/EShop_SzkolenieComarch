using EShop.Data.Entities;

namespace EShop.Data.Context
{
    public static class Seeder
    {
        public static void Seed(ApplicationDbContext dbContext)
        {
            Bogus.Faker faker = new();

            for (int i = 0; i < 100; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Name = faker.Commerce.ProductName(),
                    EAN = faker.Commerce.Ean13(),
                    PriceNet = faker.Random.Decimal(1, 1000),
                    Type = (ProductTypes)faker.Random.Int(0, 4),
                    Id = faker.UniqueIndex
                });
            }

            for (int i = 0; i < 100; i++)
            {
                dbContext.Customers.Add(new Customer
                {
                    Id = faker.UniqueIndex,
                    Name = faker.Company.CompanyName(),
                    Region = (Regions)faker.Random.Int(0, 1),
                });
            }

            dbContext.SaveChanges();
        }
    }
}
