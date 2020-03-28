using Newtonsoft.Json;

namespace Avalanche.Net.Models.Api
{
    public class MinterSets
    {
        [JsonProperty(PropertyName = "minters", Order = 0)]
        public string[] Minters { get; set; }

        [JsonProperty(PropertyName = "threshold", Order = 1)]
        public int Threshold { get; set; }
    }
}