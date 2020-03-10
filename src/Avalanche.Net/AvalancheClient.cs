using Avalanche.Net.Api;

namespace Avalanche.Net
{
    public class AvalancheClient
    {
        private readonly string protocol;
        private readonly string ipAddress;
        private readonly string port;

        public Admin Admin { get; private set; }
        public Keystore Keystore { get; private set; }
        public Avm Avm { get; private set; }

        public AvalancheClient(string protocol, string ipAddress, string port)
        {
            this.protocol = protocol;
            this.ipAddress = ipAddress;
            this.port = port;

            Admin = new Admin(protocol, ipAddress, port);
            Keystore = new Keystore(protocol, ipAddress, port);
            Avm = new Avm(protocol, ipAddress, port);
        }
    }
}