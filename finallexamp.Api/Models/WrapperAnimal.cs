using System.Text.Json.Serialization;

namespace finallexamp.Api.Models
{
    public class WrapperAnimal
    {
        [JsonPropertyName("results")]
        public List<AnimalApi> Animals { get; set; } = new List<AnimalApi>();
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
