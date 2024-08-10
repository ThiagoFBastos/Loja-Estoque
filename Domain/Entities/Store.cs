using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Store
    {
        public Guid Id { get; private init; }

        [StringLength(256, MinimumLength = 1)]
        public required string Name { get; set; }

        [StringLength(300, MinimumLength = 1)]
        public required string Address { get; set; }
        public List<Product> Products { get; } = [];
        public List<StockItem> StockItems { get; } = [];
    }
}
