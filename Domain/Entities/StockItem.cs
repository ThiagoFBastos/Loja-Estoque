
namespace Domain.Entities
{
    public class StockItem
    {
        public Guid Id { get; private init; }
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
        public required uint Quantity { get; set; }
        public Product? Product { get; set; }
        public Store? Store { get; set; }
    }
}
