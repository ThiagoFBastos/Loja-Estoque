using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record StockItemForUpdateDto
    {
        [JsonPropertyName("quantity")]
        /// <summary>
        /// Quantity of products at stock
        /// </summary>
        public required uint Quantity { get; set; }
    }
}
