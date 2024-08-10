using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StockItemRepository: RepositoryBase<StockItem>, IStockItemRepository
    {
        public StockItemRepository(RepositoryContext context) : base(context) { }
        public void AddStockItem(StockItem stockItem) => Add(stockItem);
        public void DeleteStockItem(StockItem stockItem) => Delete(stockItem);
        public Task<StockItem?> GetStockItemFromStoreAsync(Guid storeId, Guid productId)
            => FindByCondition(e => e.StoreId == storeId && e.ProductId == productId)
                .FirstOrDefaultAsync<StockItem?>();

        public void UpdateStockItem(StockItem stockItem) => Update(stockItem);

        public Task<StockItem?> GetStockItemByIdAsync(Guid id)
            => FindByCondition(e => e.Id == id)
                .FirstOrDefaultAsync<StockItem?>();
     }
}
