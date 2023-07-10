using CrudAPI.Models;

namespace CrudAPI.Repositories
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        List<Product> GetProductsByUserId(int userId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        bool AnyProducts();
        object GetAllProducts();
    }
}