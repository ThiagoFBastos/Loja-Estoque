using AutoMapper;
using Domain.Repositories;
using Services.Contracts;
using Shared.Dtos;
using Domain.Entities;
using Domain.Exceptions;

namespace Services
{
    public class ProductService: IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ProductService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProduct(ProductForCreateDto product)
        {
            Product entity = _mapper.Map<Product>(product);

            _repository.ProductRepository.AddProduct(entity);
            await _repository.SaveAsync();

            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto> UpdateProduct(Guid id, ProductForUpdateDto product)
        {
            Product? entity = await _repository.ProductRepository.GetProductByIdAsync(id);

            if (entity is null)
                throw new ProductNotFoundException(id);

            _mapper.Map(product, entity, typeof(ProductForUpdateDto), typeof(Product));
            _repository.ProductRepository.UpdateProduct(entity);
           await _repository.SaveAsync();

            return _mapper.Map<ProductDto>(entity);
        }

        public async Task DeleteProduct(Guid id)
        {
            Product? product = await _repository.ProductRepository.GetProductByIdAsync(id);

            if (product is null)
                throw new ProductNotFoundException(id);

            _repository.ProductRepository.DeleteProduct(product);
            await _repository.SaveAsync();
        }

        public async Task<ProductDto?> GetProduct(Guid id)
        {
            Product? product = await _repository.ProductRepository.GetProductByIdAsync(id);

            if (product is null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            List<Product> products = await _repository.ProductRepository.GetAllProductsAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
