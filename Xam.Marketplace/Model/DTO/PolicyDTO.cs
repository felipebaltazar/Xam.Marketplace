using Newtonsoft.Json;

namespace Xam.Marketplace.Model
{
    public sealed class PolicyDTO
    {
        [JsonProperty("discount")]
        public float Discount { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }
    }
}
