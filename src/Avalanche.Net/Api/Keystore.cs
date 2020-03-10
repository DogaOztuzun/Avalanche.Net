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

        public async Task<ApiResponse<string>> CreateUser(string username, string password)
        {
            var parameters = new Dictionary<string, string>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("keystore.createUser", parameters));
        }

        public async Task<ApiResponse<string[]>> ListUsers()
        {
            return await SendAsync<string[]>(new ApiRequest("keystore.listUsers", null));
        }

        public async Task<ApiResponse<string>> ExportUser(string username, string password)
        {
            var parameters = new Dictionary<string, string>{
                    {"username", username},
                    {"password", password}
                };

            return await SendAsync<string>(new ApiRequest("keystore.exportUser", parameters));
        }
        public async Task<ApiResponse<string>> ImportUser(string username, string password, string user)
        {
            var parameters = new Dictionary<string, string>{
                    {"username", username},
                    {"password", password},
                    {"user", user}
                };

            return await SendAsync<string>(new ApiRequest("keystore.importUser", parameters));
        }
    }
}