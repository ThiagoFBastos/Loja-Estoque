using Domain.Repositories;
using Shared.Dtos;

namespace Services.Contracts
{
    public interface IStockItemService
    {
        Task<StockItemDto> AddStockItem(StockItemForCreateDto stockItem);
        Task DeleteStockItem(Guid id);
        Task<StockItemDto> AddItemsFromStockItem(Guid id, StockItemForUpdateDto stockItem);
        Task<StockItemDto> RemoveItemsFromStockItem(Guid id, StockItemForUpdateDto stockItem);
        Task<StockItemDto?> GetStockItem(Guid id);
    }
}
