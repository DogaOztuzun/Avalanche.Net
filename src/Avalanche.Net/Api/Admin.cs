using System.Collections.Generic;
using System.Threading.Tasks;
using Avalanche.Net.Models;

namespace Avalanche.Net.Api
{
    public class Admin : ApiBase
    {
        public Admin(string protocol, string ipAddress, string port) : base(protocol, ipAddress, port, "ext/admin")
        {

        }

        public async Task<ApiResponse<string[]>> PeersAsync()
        {
            return await SendAsync<string[]>(new ApiRequest("admin.peers", null));
        }

        public async Task<ApiResponse<string>> GetNetworkIdAsync()
        {
            return await SendAsync<string>(new ApiRequest("admin.getNetworkID", null));
        }

        public async Task<ApiResponse<string>> AliasAsync(string alias, string endpoint)
        {
            var parameters = new Dictionary<string, string>{
                {"alias", alias},
                {"endpoint", endpoint}
            };

            return await SendAsync<string>(new ApiRequest("admin.alias", parameters));
        }

        public async Task<ApiResponse<string>> AliasChainAsync(string chain, string alias)
        {
            var parameters = new Dictionary<string, string>{
                {"chain", chain},
                {"alias", alias}
            };

            return await SendAsync<string>(new ApiRequest("admin.aliasChain", parameters));
        }

        public async Task<ApiResponse<string>> GetBlockchainIdAsync(string alias)
        {
            var parameters = new Dictionary<string, string>{
                {"alias", alias}
            };

            return await SendAsync<string>(new ApiRequest("admin.getBlockchainID", parameters));
        }

        public async Task<ApiResponse<string>> StartCpuProfilerAsync(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.startCPUProfiler", parameters));
        }

        public async Task<ApiResponse<string>> StopCpuProfilerAsync()
        {
            return await SendAsync<string>(new ApiRequest("admin.stopCPUProfiler", null));
        }

        public async Task<ApiResponse<string>> MemoryProfileAsync(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.memoryProfile", parameters));
        }

        public async Task<ApiResponse<string>> LockProfileAsync(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.lockProfile", parameters));
        }
    }
}