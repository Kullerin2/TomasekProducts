using TomasekRestApi.Models;

namespace TomasekRestApi.Data
{

    public interface IProductRepository
    {
        List<Product> GetProducts();
        List<Product> GetProducts(int page, int pageSize);
        void UpdateProductDescr(int id, string desc);
        Product GetProductById(int id);
    }

}
