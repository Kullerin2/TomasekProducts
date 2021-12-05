
using TomasekRestApi.Models;

namespace TomasekRestApi.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AlzaTestDBContext db;

        public ProductRepository(AlzaTestDBContext db)
        {
            this.db = db;
        }

        public List<Product> GetProducts() => db.Products.ToList();

        public List<Product> GetProducts(int page,int pageSize=10)
        {
            return db.Products.Skip(page * pageSize).Take(pageSize).ToList();
            
        }
        public Product GetProductById(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                product = new Product();
            }
            return product;
            //return db.Products.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateProductDescr(int id,string desc)
        {
            var product =db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                product.Description = desc;
                db.SaveChanges();                
            }
        }

        
    }
}
