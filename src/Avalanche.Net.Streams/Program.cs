using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalanche.Net.Models.Avm;
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
            var tx = new TxUnsigned();
            tx.FromBuffer(buffer);
        }
    }
}