using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Avalanche.Net.Models
{
    public class ApiRequest
    {
        public ApiRequest(string method, Dictionary<string, string> param)
        {
            Method = method;
            Params = param;
        }

        [JsonProperty(PropertyName = "jsonrpc", Order = 0)]
        public string JsonRpc { get; set; } = "2.0";

        [JsonProperty(PropertyName = "id", Order = 1)]
        public int Id { get; set; } = 1;

        [JsonProperty(PropertyName = "method", Order = 2)]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params", Order = 3)]
        public Dictionary<string, string> Params { get; set; }

        public string GetString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(GetString());
        }
    }
}