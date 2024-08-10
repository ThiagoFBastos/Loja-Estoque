using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public record ProductForUpdateDto
    {
        [JsonPropertyName("name")]
        /// <summary>
        ///  Product name with length between 1 and 256.
        /// </summary>
        public required string Name { get; set; }

        [JsonPropertyName("price")]
        /// <summary>
        /// Price of product
        /// </summary>
        public required decimal Price { get; set; }
    }
}
