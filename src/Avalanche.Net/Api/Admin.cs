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

        public async Task<ApiResponse<string[]>> Peers()
        {
            return await SendAsync<string[]>(new ApiRequest("admin.peers", null));
        }

        public async Task<ApiResponse<string>> GetNetworkId()
        {
            return await SendAsync<string>(new ApiRequest("admin.getNetworkID", null));
        }

        public async Task<ApiResponse<string>> Alias(string alias, string endpoint)
        {
            var parameters = new Dictionary<string, string>{
                {"alias", alias},
                {"endpoint", endpoint}
            };

            return await SendAsync<string>(new ApiRequest("admin.alias", parameters));
        }

        public async Task<ApiResponse<string>> AliasChain(string chain, string alias)
        {
            var parameters = new Dictionary<string, string>{
                {"chain", chain},
                {"alias", alias}
            };

            return await SendAsync<string>(new ApiRequest("admin.aliasChain", parameters));
        }

        public async Task<ApiResponse<string>> GetBlockchainId(string alias)
        {
            var parameters = new Dictionary<string, string>{
                {"alias", alias}
            };

            return await SendAsync<string>(new ApiRequest("admin.getBlockchainID", parameters));
        }

        public async Task<ApiResponse<string>> StartCpuProfiler(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.startCPUProfiler", parameters));
        }

        public async Task<ApiResponse<string>> StopCpuProfiler()
        {
            return await SendAsync<string>(new ApiRequest("admin.stopCPUProfiler", null));
        }

        public async Task<ApiResponse<string>> MemoryProfile(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.memoryProfile", parameters));
        }

        public async Task<ApiResponse<string>> LockProfile(string filename)
        {
            var parameters = new Dictionary<string, string>{
                {"fileName", filename}
            };

            return await SendAsync<string>(new ApiRequest("admin.lockProfile", parameters));
        }
    }
}