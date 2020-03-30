using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalanche.Net.Models.Avm;
using NBitcoin.Crypto;
using nng;

namespace Avalanche.Net.Streams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "ipc:///tmp/i8KtK2KwLi1o7WaVBbEKpRLPtAEYayfoptqAFYxfQgrus1g6m.ipc";
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var ctx = new NngLoadContext(path);
            var factory = NngLoadContext.Init(ctx);

            using var sock = factory.SubscriberOpen().Unwrap();
            sock.Dial(url).Unwrap();
            sock.SetOpt("sub:subscribe", new byte[] { });

            var cancellationSource = new CancellationTokenSource();
            
            await Task.Run(() =>
            {
                while (!cancellationSource.IsCancellationRequested)
                {
                    var msg = sock.RecvMsg().Unwrap();
                    var buffer = msg.AsSpan().ToArray();
                    
                    BytesToTx(buffer);
                }
            }, cancellationSource.Token);
        }

        private static void BytesToTx(byte[] buffer)
        {
            var tx = new Tx();
            tx.FromBuffer(buffer);
            
            Console.WriteLine(tx.TxId);

            Console.WriteLine(tx.TxUnsigned.GetNetworkId());
            Console.WriteLine(tx.TxUnsigned.GetBlockChainId());
            
            Console.WriteLine("Inputs");
            foreach (var input in tx.TxUnsigned.GetInputs())
            {
                if (input is SecpInput secpInput)
                {
                    Console.WriteLine("AssetId: "+secpInput.GetAssetId());
                    Console.WriteLine("Amount: "+secpInput.AmountValue);    
                }
            }
            
            Console.WriteLine("Outputs");
            foreach (var output in tx.TxUnsigned.GetOutputs())
            {
                if (!(output is SecpOutput secp)) continue;
                
                Console.WriteLine("AssetId: "+secp.GetAssetId());
                Console.WriteLine("Amount: "+secp.AmountValue);
                Console.WriteLine(secp.GetAddresses().First());
            }
        }
    }
}