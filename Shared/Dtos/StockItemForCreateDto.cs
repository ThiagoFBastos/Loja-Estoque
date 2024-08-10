using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record StockItemForCreateDto
    {
        [JsonPropertyName("storeId")]
        /// <summary>
        /// Store id that stock item belong
        /// </summary>
        public Guid StoreId { get; set; }

        [JsonPropertyName("productId")]
        /// <summary>
        /// Product id of stock item
        /// </summary>
        public Guid ProductId { get; set; }

        [JsonPropertyName("quantity")]
        /// <summary>
        /// Quantity of products at stock
        /// </summary>
        public required uint Quantity { get; set; }
    }
}
