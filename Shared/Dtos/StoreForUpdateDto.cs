using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record StoreForUpdateDto
    {
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
    }
}
