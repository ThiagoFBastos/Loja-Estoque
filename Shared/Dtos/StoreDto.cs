using System.Text.Json.Serialization;
using System.Text.Json;

namespace Shared.Dtos
{
    public record StoreDto
    {
        [JsonPropertyName("id")]
        /// <summary>
        /// Store unique id
        /// </summary>
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        /// <summary>
        /// Store name with length between 1 and 256.
        /// </summary>
        public required string Name { get; set; }

        [JsonPropertyName("address")]
        /// <summary>
        /// Store adress with length between 1 and 300.
        /// </summary>
        public required string Address { get; set; }

        [JsonPropertyName("stockItems")]
        /// <summary>
        /// List of stock items
        /// </summary>
        public List<StockItemDto> StockItems { get; } = [];

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
