using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<List<Product>> GetAllProductsAsync();
    }
}
