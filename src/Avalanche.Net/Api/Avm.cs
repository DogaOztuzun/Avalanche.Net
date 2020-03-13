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
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.createAddress", parameters));
        }

        public async Task<ApiResponse<string>> GetBalanceAsync(string address, string assetId)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"address", address},
                    {"assetID", assetId}
                };

            return await SendAsync<string>(new ApiRequest("avm.getBalance", parameters));
        }

        public async Task<ApiResponse<string>> GetUTXOsAsync(string[] addresses)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"addresses", addresses}
                };

            return await SendAsync<string>(new ApiRequest("avm.getUTXOs", parameters));
        }

        public async Task<ApiResponse<string>> IssueTxAsync(string tx)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"tx", tx}
                };

            return await SendAsync<string>(new ApiRequest("avm.issueTx", parameters));
        }

        public async Task<ApiResponse<string>> SignMintTxAsync(string tx, string minter, string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"tx", tx},
                    {"minter", minter},
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.signMintTx", parameters));
        }

        public async Task<ApiResponse<string>> GetTxStatusAsync(string txId)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"txID", txId}
                };

            return await SendAsync<string>(new ApiRequest("avm.getTxStatus", parameters));
        }

        public async Task<ApiResponse<string>> SendAsync(string assetId, int amount, string to, string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"assetID", assetId},
                    {"amount", amount},
                    {"to", to},
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.send", parameters));
        }

        public async Task<ApiResponse<string>> CreateFixedCapAssetAsync(string name, string symbol, int denomination, InitialHolders[] initialHolders, string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"name", name},
                    {"symbol", symbol},
                    {"denomination", denomination},
                    {"initialHolders", initialHolders},
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.createFixedCapAsset", parameters));
        }

        public async Task<ApiResponse<string>> CreateVariableCapAssetAsync(string name, string symbol, int denomination, MinterSets[] minterSets, string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"name", name},
                    {"symbol", symbol},
                    {"denomination", denomination},
                    {"minterSets", minterSets},
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("avm.createVariableCapAsset", parameters));
        }

        public async Task<ApiResponse<string>> CreateMintTxAsync(int amount, string assetId, string to, string[] minters)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"amount", amount},
                    {"assetID", assetId},
                    {"to", to},
                    {"minters", minters}
                };

            return await SendAsync<string>(new ApiRequest("avm.createMintTx", parameters));
        }

        public async Task<ApiResponse<string>> GetAssetDescriptionAsync(string assetId)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"assetID", assetId}
                };

            return await SendAsync<string>(new ApiRequest("avm.getAssetDescription", parameters));
        }

        public async Task<ApiResponse<string>> ExportKeyAsync(string username, string password, string address)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password},
                    {"address", address}
                };

            return await SendAsync<string>(new ApiRequest("avm.exportKey", parameters));
        }

        public async Task<ApiResponse<string>> ImportKeyAsync(string username, string password, string privateKey)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password},
                    {"privateKey", privateKey}
                };

            return await SendAsync<string>(new ApiRequest("avm.importKey", parameters));
        }
    }
}
