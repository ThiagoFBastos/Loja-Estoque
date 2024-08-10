using Domain.Repositories;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class RepositoryManager: IRepositoryManager
    {
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IStoreRepository> _storeRepository;
        private readonly Lazy<IStockItemRepository> _stockItemRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context)
        {
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            _storeRepository = new Lazy<IStoreRepository>(() => new StoreRepository(context));
            _stockItemRepository = new Lazy<IStockItemRepository>(() => new StockItemRepository(context));
            _context = context;
        }

        public IProductRepository ProductRepository => _productRepository.Value;
        public IStoreRepository StoreRepository => _storeRepository.Value;
        public IStockItemRepository StockItemRepository => _stockItemRepository.Value;
        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
