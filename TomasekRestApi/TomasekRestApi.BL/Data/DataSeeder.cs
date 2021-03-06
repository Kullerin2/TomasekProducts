using TomasekRestApi.Model.Models;

namespace TomasekRestApi.BL.Data
{
    public class DataSeeder
    {
        private readonly AlzaTestDBContext dbContext;

        public DataSeeder(AlzaTestDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (!dbContext.Products.Any())
            {
                var Products = GetSetOfTestingProducts();
                dbContext.Products.AddRange(Products);
                dbContext.SaveChanges();
            }
        }
        public static List<Product> GetSetOfTestingProducts()
        {
            var Products = new List<Product>();
            for (int i = 1; i <= 20; i++)
            {
                Product product = new Product();
                product.Name = "Test"+i;
                product.Description ="TestDesc"+i;
                product.ImgUrl = "ImgUrl"+i;
                product.Price = i *100;
                Products.Add(product);

            }
            return Products;
        }
    }
}
