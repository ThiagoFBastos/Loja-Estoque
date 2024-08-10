
namespace Domain.Exceptions
{
    public class StockItemNotFoundException: NotFoundException
    {
        public StockItemNotFoundException(Guid id): base($"Stock item {id} not found.") { }
        public StockItemNotFoundException(Guid storeId, Guid productId): base($"Stock item from Store {storeId} and Product {productId} not found.") { }
    }
}
