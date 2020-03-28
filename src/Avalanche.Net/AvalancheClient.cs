using Avalanche.Net.Api;

namespace Avalanche.Net
{
    public class AvalancheClient
    {
        private readonly string _protocol;
        private readonly string _ipAddress;
        private readonly string _port;

        public Admin Admin { get; }
        public Keystore Keystore { get; }
        public Avm Avm { get; }

        public AvalancheClient(string protocol, string ipAddress, string port)
        {
            _protocol = protocol;
            _ipAddress = ipAddress;
            _port = port;

            Admin = new Admin(protocol, ipAddress, port);
            Keystore = new Keystore(protocol, ipAddress, port);
            Avm = new Avm(protocol, ipAddress, port);
        }
    }
}