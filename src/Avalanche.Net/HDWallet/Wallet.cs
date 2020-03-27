using Avalanche.Net.Api.AVMAPI;
using NBitcoin;

namespace Avalanche.Net.HDWallet
{
    public class Wallet
    {
        // TODO: Update with AVA hd path
        const string AVA_PATH = "m/44'/0'/0'/x";

        private readonly byte[] _seed;

        public Wallet(string words, string passphrase = null)
        {
            Mnemonic mneumonic = new Mnemonic(words);
            _seed = mneumonic.DeriveSeed(passphrase);
        }

        public AVMKeyPair GetKeyPair(int index)
        {
            var masterKey = new ExtKey(_seed);
            var keyPath = new KeyPath(AVA_PATH.Replace("x", index.ToString()));
            ExtKey key = masterKey.Derive(keyPath);

            return new AVMKeyPair("", key.PrivateKey);
        }
    }
}