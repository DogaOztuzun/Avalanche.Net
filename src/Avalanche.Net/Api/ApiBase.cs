using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Avalanche.Net.Models.Api;

namespace Avalanche.Net.Api
{
    public class ApiBase
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiUrl;

        protected ApiBase(string protocol, string ipAddress, string port, string path)
        {
            _apiUrl = $"{protocol}://{ipAddress}:{port}/{path}";
        }

        protected async Task<T> SendAsync<T>(ApiRequest request)
        {
            var data = new StringContent(request.GetString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl, data);

            var responseData = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseData);
        }
    }
}
