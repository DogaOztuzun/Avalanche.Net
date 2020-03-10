using System.Collections.Generic;
using System.Threading.Tasks;
using Avalanche.Net.Models;

namespace Avalanche.Net.Api
{
    public class Avm : ApiBase
    {
        public Avm(string protocol, string ipAddress, string port) : base(protocol, ipAddress, port, "ext/bc/X")
        {

        }

        public async Task<ApiResponse<string>> CreateAddressAsync(string username, string password)
        {
            var parameters = new Dictionary<string, string>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.createAddress", parameters));
        }

        public async Task<ApiResponse<string>> GetBalanceAsync(string address, string assetId)
        {
            var parameters = new Dictionary<string, string>{
                    {"address", address},
                    {"assetID", assetId}
                };

            return await SendAsync<string>(new ApiRequest("avm.getBalance", parameters));
        }

        public async Task<ApiResponse<string>> ExportKeyAsync(string username, string password, string address)
        {
            var parameters = new Dictionary<string, string>{
                    {"username", username},
                    {"password", password},
                    {"address", address}
                };

            return await SendAsync<string>(new ApiRequest("avm.exportKey", parameters));
        }
    }
}
