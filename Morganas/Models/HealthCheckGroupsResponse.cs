using System.Text.Json.Serialization;
namespace Morganas.Models
{
    public class HealthCheckGroupsResponse{
        [JsonPropertyName("items")]
        public List<Check> Checks { get; set; } = new List<Check>();
        [JsonPropertyName("total")]
        public int Total{ get; set;}
    }

    public class Check{
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}