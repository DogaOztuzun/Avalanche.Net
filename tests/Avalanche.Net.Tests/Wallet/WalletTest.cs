using System;
using System.Linq;
using System.Text;
using Avalanche.Net.Api.AVMAPI;
using Avalanche.Net.HDWallet;
using NBitcoin;
using NBitcoin.DataEncoders;
using NUnit.Framework;
using NBitcoin.Crypto;

namespace Tests
{
    public class WalletTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldCreateHDWallet()
        {
            var words = "clever glove portion swing nerve bullet boil rose motion nose rocket tube color account enhance";
            var passPhrase = "P@ssw0rd";

            var wallet = new Wallet(words, passPhrase);
            var key0 = wallet.GetKeyPair(0);
            key0.SetChainID("X");

            // TODO: Addresses will change when hdPath updated from BTC (0) to AVA (?)
            Assert.AreEqual("X-HuBC5rHXPnKzAt1JGBUpaKbGdLa12pB9z", key0.GetAddressString());
            Assert.AreEqual("ava1h9szvnarh5cw85unz8un5p3hdrctt73gfwuluq", key0.GetBech32Address());
        }

        [Test]
        public void ShouldSignAndVerifyWithNewKey()
        {
            var words = "clever glove portion swing nerve bullet boil rose motion nose rocket tube color account enhance";
            var passPhrase = "P@ssw0rd";

            var wallet = new Wallet(words, passPhrase);
            var kp = wallet.GetKeyPair(0);
            
            var hashed = Hashes.SHA256("09090909".HexToBytes());
            var signed = kp.Sign(hashed); 
            Assert.IsTrue(kp.Verify(hashed, signed));
        }
    }
}