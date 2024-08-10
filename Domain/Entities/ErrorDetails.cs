using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ErrorDetails
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("traceId")]
        public string? TraceId { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
