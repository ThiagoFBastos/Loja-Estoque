using Domain.Entities;

namespace Domain.Repositories
{
    public interface IStockItemRepository
    {
        void AddStockItem(StockItem stockItem);
        void DeleteStockItem(StockItem stockItem);
        Task<StockItem?> GetStockItemByIdAsync(Guid id);
        Task<StockItem?> GetStockItemFromStoreAsync(Guid storeId, Guid productId);
        void UpdateStockItem(StockItem stockItem);
    }
}
