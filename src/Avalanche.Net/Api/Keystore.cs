using System.Collections.Generic;
using System.Threading.Tasks;
using Avalanche.Net.Models;

namespace Avalanche.Net.Api
{
    public class Keystore : ApiBase
    {
        public Keystore(string protocol, string ipAddress, string port) : base(protocol, ipAddress, port, "ext/keystore")
        {

        }

        public async Task<ApiResponse<string>> CreateUserAsync(string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("keystore.createUser", parameters));
        }

        public async Task<ApiResponse<string[]>> ListUsersAsync()
        {
            return await SendAsync<string[]>(new ApiRequest("keystore.listUsers", null));
        }

        public async Task<ApiResponse<string>> ExportUserAsync(string username, string password)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("keystore.exportUser", parameters));
        }

        public async Task<ApiResponse<string>> ImportUserAsync(string username, string password, string user)
        {
            var parameters = new Dictionary<string, dynamic>{
                    {"username", username},
                    {"password", password},
                    {"user", user}
                };

            return await SendAsync<string>(new ApiRequest("keystore.importUser", parameters));
        }
    }
}
