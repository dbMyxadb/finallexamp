using System.Text.Json.Serialization;

namespace finallexamp.DAL.Models
{

    public class Animal
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } 
        [JsonPropertyName("common_name")]
        public string? Name { get; set; }
        [JsonPropertyName("scientific_name")]
        public string? LatinName { get; set; }
        [JsonPropertyName("iso_code")]
        public string? CountryCode { get; set; }
    }
}
