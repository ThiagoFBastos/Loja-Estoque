using Domain.Repositories;
using Shared.Dtos;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> AddProduct(ProductForCreateDto product);
        Task<ProductDto> UpdateProduct(Guid id, ProductForUpdateDto product);
        Task DeleteProduct(Guid id);
        Task<ProductDto?> GetProduct(Guid id);
        Task<List<ProductDto>> GetAllProducts();
    }
}
