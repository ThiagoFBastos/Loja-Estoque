using Services.Contracts;
using Domain.Repositories;
using AutoMapper;

namespace Services
{
    public class ServiceManager: IServiceManager
    {
        private readonly Lazy<IStoreService> _storeService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IStockItemService> _stockItemService;

        public ServiceManager(IRepositoryManager repository, IMapper mapper)
        {
            _storeService = new Lazy<IStoreService>(() => new StoreService(repository, mapper));
            _productService = new Lazy<IProductService>(() => new ProductService(repository, mapper));
            _stockItemService = new Lazy<IStockItemService>(() => new StockItemService(repository, mapper));
        }

        public IStoreService StoreService => _storeService.Value;
        public IProductService ProductService => _productService.Value;
        public IStockItemService StockItemService => _stockItemService.Value;
    }
}
