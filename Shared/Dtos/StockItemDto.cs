using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record StockItemDto
    {
        [JsonPropertyName("id")]
        /// <summary>
        /// Stock item unique id
        /// </summary>
        public Guid Id { get; set; }

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
