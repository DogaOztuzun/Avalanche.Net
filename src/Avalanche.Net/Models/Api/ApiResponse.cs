using System.Collections.Generic;
using Newtonsoft.Json;

namespace Avalanche.Net.Models.Api
{
    public class ApiResponse<T>
    {

        [JsonProperty(PropertyName = "jsonrpc", Order = 0)]
        public string JsonRpc { get; set; } = "2.0";

        [JsonProperty(PropertyName = "id", Order = 1)]
        public int Id { get; set; } = 1;

        [JsonProperty(PropertyName = "error", Order = 3)]
        public Dictionary<string, string> Error { get; set; }

        [JsonProperty(PropertyName = "result", Order = 3)]
        public T Result { get; set; }

        public bool IsSuccessful { get { return Error == null; } }
    }
}