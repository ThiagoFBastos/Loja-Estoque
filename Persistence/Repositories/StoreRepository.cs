using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StoreRepository: RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(RepositoryContext context) : base(context) { }
        public void AddStore(Store store) => Add(store);
        public void UpdateStore(Store store) => Update(store);
        public void DeleteStore(Store store) => Delete(store);
        public Task<Store?> GetStoreByIdAsync(Guid id)
            => FindByCondition(e => e.Id == id)
                .Include(e => e.StockItems)
                .FirstOrDefaultAsync<Store?>();

        public Task<List<Store>> GetAllStoresAsync()
            => FindAll()
                .Include(e => e.StockItems)
                .ToListAsync();
    }
}
