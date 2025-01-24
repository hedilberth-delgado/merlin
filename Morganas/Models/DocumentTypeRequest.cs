using System.Text.Json.Serialization;

namespace Morganas.Models
{
    public class DocumentTypeRequest
    {
        [JsonPropertyName("alias")]
        public string? Alias { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
        [JsonPropertyName("allowedAsRoot")]
        public bool AllowedAsRoot { get; set; }
        [JsonPropertyName("variesByCulture")]
        public bool VariesByCulture { get; set; }
        [JsonPropertyName("variesBySegment")]
        public bool VariesBySegment { get; set; }
        [JsonPropertyName("collection")]
        public string? Collection { get; set; }
        [JsonPropertyName("isElement")]
        public bool IsElement { get; set; }
    }
}