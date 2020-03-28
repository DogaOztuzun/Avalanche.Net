using System;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalanche.Net.Models;
using Avalanche.Net.Models.Api;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ava = new Avalanche.Net.AvalancheClient("http", "127.0.0.1", "9650");

            // List Peers for node
            var peers = await ava.Admin.PeersAsync();
            foreach (var peer in peers.Result.Peers)
                Console.WriteLine(peer);

            // Blockchain Id
            var bId = await ava.Admin.GetBlockchainIdAsync("X");
            Console.WriteLine(bId.Result.BlockchainId);

            // Network Id
            var nId = await ava.Admin.GetNetworkIdAsync();
            Console.WriteLine(nId.Result.NetworkId);

            // Create random username
            var username = Guid.NewGuid().ToString();
            var password = "p4ssw0rd?";
            string address;
            var userResponse = await ava.Keystore.CreateUserAsync(username, password);

            if (userResponse.IsSuccessful)
            {
                Console.WriteLine($"User: {username}");

                // Create address
                var addressResponse = await ava.Avm.CreateAddressAsync(username, password);

                if (addressResponse.IsSuccessful)
                {
                    address = addressResponse.Result.Address;
                    Console.WriteLine($"Address: {address}");
                }
            }

            // List users
            var users = await ava.Keystore.ListUsersAsync();
            foreach (var user in users.Result.Users)
                Console.WriteLine(user);

            var balance = await ava.Avm.GetBalanceAsync("X-8oYdtDYoPB4TPmxexM1HzHb1Gfx2wpLXE", "AVA");
            Console.WriteLine(balance.Result.Balance);

            // var importedKey = await ava.Avm.ImportKeyAsync("osman", "akzx1234!", "vZV7APG8SgkUdHFmP6tvVm3Luv1fyKLUq98j7haDbWMWjdgkE");


            var holders = new InitialHolders[]{
                new InitialHolders{
                    Address = "X-8oYdtDYoPB4TPmxexM1HzHb1Gfx2wpLXE",
                    Amount= 10000
                }};
            // var asset = await ava.Avm.CreateFixedCapAssetAsync("test XXX", "XXX", 0, holders, "osman", "akzx1234!");
            // Console.WriteLine(asset.Result["assetID"]);
            var minters = new MinterSets[] {
                new MinterSets{
                    Minters = new string[]{"X-8oYdtDYoPB4TPmxexM1HzHb1Gfx2wpLXE"},
                    Threshold = 1
                },
                // new MinterSets{
                //     Minters = new string[]{"X-LzjQwB4EffxzPi571VfcqAbsuwwDudJCT"},
                //     Threshold = 2
                // }
             };
            // var varCap = await ava.Avm.CreateVariableCapAssetAsync("test Corp", "CSX", 0, minters, "osman", "akzx1234!");
            // Console.WriteLine(varCap.Result["assetID"]);

            // var mintTx = await ava.Avm.CreateMintTxAsync(10000, "28JEgRZ3pYoke1bcKtDDh8Dy6Dcbn9ZtS8fetg4ANrQwNhbwUA", "X-LzjQwB4EffxzPi571VfcqAbsuwwDudJCT", new string[] { "X-8oYdtDYoPB4TPmxexM1HzHb1Gfx2wpLXE" });
            // Console.WriteLine(mintTx.Result["tx"]);
            // var mintTxId = mintTx.Result["tx"];

            // var signedMintTx = await ava.Avm.SignMintTxAsync(mintTxId, "X-8oYdtDYoPB4TPmxexM1HzHb1Gfx2wpLXE", "osman", "akzx1234!");
            // Console.WriteLine(signedMintTx.Result["tx"]);
            // var signedMintTxId = "111PubD8Wqe5uYWj2kF3qhfcGsnRYYLUxEjgHDLXE5FUJmnmqTAp7Hb5BDADpSyPY25d4Veh2BCCKR9FwL4DFvg4r6tTwG8XgP6MpXzZC42c1JyjVLnjqFUYWuHAv9BviwpHq5UynwQ54pbqxvfx84WfKnKgM7GWhbYH4J9DZw6W7DsZSuiaMRh8BcgwDZHtuULdoiHabBjJV6E2ySUsse1gKCm7jjSKbJJUuLE489BK26r5gcfstiUE1J3kyvNeuNbdR2f9yxPP21bno2ZCRjGiadzRTcuehcsuQmT2ZEmAKMVKbNDuxw5vjvk7RiPKNvLmmzriv4TtuqLNgmTNaEEgVsK5kS8J23ZfynBnitqWkVUTBbQYNyKkyQPHzByrSsvPdX9zXo1XoLM9UAKTjprjB";

            // var xx = await ava.Avm.IssueTxAsync(signedMintTxId);
        }
    }
}