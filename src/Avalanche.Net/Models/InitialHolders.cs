using Newtonsoft.Json;

namespace Avalanche.Net.Models
{
    public class InitialHolders
    {
        [JsonProperty(PropertyName = "address", Order = 0)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "amount", Order = 1)]
        public int Amount { get; set; }
    }
}