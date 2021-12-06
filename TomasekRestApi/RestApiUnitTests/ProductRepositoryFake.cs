using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomasekRestApi.BL.Data;
using TomasekRestApi.Model.Models;

namespace RestApiUnitTests
{
    public class ProductRepositoryFake : IProductRepository
    {
        List<Product> _products;    
        //Prepare mock repository
        public ProductRepositoryFake()
        {
            _products = DataSeeder.GetSetOfTestingProducts();
            for(int i=0; i < _products.Count; i++)
            {
                _products[i].Id = i+1;
            }

        }

        Product IProductRepository.GetProductById(int id)
        {
            return  _products.FirstOrDefault(p => p.Id == id);
        }

        List<Product> IProductRepository.GetProducts()
        {
            return _products.ToList();
        }

        List<Product> IProductRepository.GetProducts(int page, int pageSize)
        {
            return _products.Skip(page * pageSize).Take(pageSize).ToList();
        }

        void IProductRepository.UpdateProductDescr(int id, string desc)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                product.Description = desc;                
            }
        }
    }
}
