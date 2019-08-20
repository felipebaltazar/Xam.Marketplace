using Newtonsoft.Json;

namespace Xam.Marketplace.Model
{
    public sealed class OfferDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("policies")]
        public PolicyDTO[] Policies { get; set; }
    }
}
