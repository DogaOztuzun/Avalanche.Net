using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Avalanche.Net.Models;

namespace Avalanche.Net.Api
{
    public class ApiBase
    {
        private HttpClient _client = new HttpClient();
        private string apiUrl;

        protected ApiBase(string protocol, string ipAddress, string port, string path)
        {
            apiUrl = $"{protocol}://{ipAddress}:{port}/{path}";
        }

        protected async Task<ApiResponse<T>> SendAsync<T>(ApiRequest request)
        {
            var data = new StringContent(request.GetString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(apiUrl, data);

            var responseData = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<T>>(responseData);
        }
    }
}
