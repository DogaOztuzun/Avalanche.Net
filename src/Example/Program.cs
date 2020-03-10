using System;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ava = new Avalanche.Net.AvalancheClient("http", "127.0.0.1", "9650");

            // List Peers for node
            var peers = await ava.Admin.Peers();
            foreach (var peer in peers.Result["peers"])
                Console.WriteLine(peer);

            // Random username
            var username = Guid.NewGuid().ToString();
            await ava.Keystore.CreateUser(username, "p4ssw0rd?");

            // List users
            var users = await ava.Keystore.ListUsers();
            foreach (var user in users.Result["users"])
                Console.WriteLine(user);

        }
    }
}
