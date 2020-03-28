using System.Collections.Generic;
using System.Threading.Tasks;
using Avalanche.Net.Models.Api;

namespace Avalanche.Net.Api
{
    public class Admin : ApiBase
    {
        public Admin(string protocol, string ipAddress, string port) : base(protocol, ipAddress, port, "ext/admin")
        {

        }

        public async Task<ApiResponse<PeersResponse>> PeersAsync()
        {
            return await SendAsync<ApiResponse<PeersResponse>>(new ApiRequest("admin.peers", null));
        }

        public async Task<ApiResponse<NetworkIdResponse>> GetNetworkIdAsync()
        {
            return await SendAsync<ApiResponse<NetworkIdResponse>>(new ApiRequest("admin.getNetworkID", null));
        }

        public async Task<ApiResponseBag<string>> AliasAsync(string alias, string endpoint)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"alias", alias},
                {"endpoint", endpoint}
            };

            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.alias", parameters));
        }

        public async Task<ApiResponseBag<string>> AliasChainAsync(string chain, string alias)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"chain", chain},
                {"alias", alias}
            };

            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.aliasChain", parameters));
        }

        public async Task<ApiResponse<BlockchainIdResponse>> GetBlockchainIdAsync(string alias)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"alias", alias}
            };

            return await SendAsync<ApiResponse<BlockchainIdResponse>>(new ApiRequest("admin.getBlockchainID", parameters));
        }

        public async Task<ApiResponseBag<string>> StartCpuProfilerAsync(string filename)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"fileName", filename}
            };

            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.startCPUProfiler", parameters));
        }

        public async Task<ApiResponseBag<string>> StopCpuProfilerAsync()
        {
            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.stopCPUProfiler", null));
        }

        public async Task<ApiResponseBag<string>> MemoryProfileAsync(string filename)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"fileName", filename}
            };

            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.memoryProfile", parameters));
        }

        public async Task<ApiResponseBag<string>> LockProfileAsync(string filename)
        {
            var parameters = new Dictionary<string, dynamic>{
                {"fileName", filename}
            };

            return await SendAsync<ApiResponseBag<string>>(new ApiRequest("admin.lockProfile", parameters));
        }
    }
}