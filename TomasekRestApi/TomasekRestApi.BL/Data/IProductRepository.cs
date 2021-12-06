using TomasekRestApi.Model.Models;

namespace TomasekRestApi.BL.Data
{

    public interface IProductRepository
    {
        List<Product> GetProducts();
        List<Product> GetProducts(int page, int pageSize=10);
        void UpdateProductDescr(int id, string desc);
        Product GetProductById(int id);
    }

}
