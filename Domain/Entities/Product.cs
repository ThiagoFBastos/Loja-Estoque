using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private init; }

        [StringLength(256, MinimumLength = 1)]
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public List<Store> Stores { get; } = [];
        public List<StockItem> StockItems { get; } = [];
    }
}
