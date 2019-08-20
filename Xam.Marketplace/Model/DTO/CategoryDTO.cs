using Newtonsoft.Json;

namespace Xam.Marketplace.Model
{
    public class CategoryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
