using Newtonsoft.Json;

namespace Xam.Marketplace.Model
{
    public sealed class ProductDTO
    {
        [JsonProperty("category_id")]
        public int? CategoryId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }
    }
}
