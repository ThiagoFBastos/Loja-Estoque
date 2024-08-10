using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record ProductDto
    {
        [JsonPropertyName("id")]
        /// <summary>
        /// Product unique Id 
        /// </summary>
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        /// <summary>
        /// Product name with length between 1 and 255.
        /// </summary>
        public required string Name { get; set; }

        [JsonPropertyName("price")]
        /// <summary>
        /// Price of product.
        /// </summary>
        public required decimal Price { get; set; }

        [JsonPropertyName("stockItems")]
        /// <summary>
        /// List of stock items
        /// </summary>
        public List<StockItemDto> StockItems { get; } = [];
    }
}
