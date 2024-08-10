using Persistence.Context;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository: RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context): base(context) { }
        public void AddProduct(Product product) => Add(product);
        public void UpdateProduct(Product product) => Update(product);
        public void DeleteProduct(Product product) => Delete(product);
        public Task<Product?> GetProductByIdAsync(Guid id)
            => FindByCondition(e => e.Id == id)
                .Include(e => e.StockItems)
                .FirstOrDefaultAsync<Product?>();

        public Task<List<Product>> GetAllProductsAsync() 
            => FindAll()
                .Include(e => e.StockItems)
                .ToListAsync();
    }
}
