using Avalanche.Net.Models.Avm;
using Avalanche.Net.Utilities;
using HDWallet.Avalanche;
using HDWallet.Core;
using NBitcoin.Crypto;
using NUnit.Framework;

namespace Avalanche.Net.Tests.Wallet
{
    public class WalletTest
    {
        [Test]
        public void ShouldCreateHDWallet()
        {
            var words = "clever glove portion swing nerve bullet boil rose motion nose rocket tube color account enhance";
            var passPhrase = "P@ssw0rd";

            IHDWallet<AvalancheWallet> hdWallet = new AvalancheHDWallet(words, passPhrase);
            var key0 = hdWallet.GetAccount(0).GetExternalWallet(0);

            Assert.AreEqual("X-avax1zcsk0ptxe72suv3w6pr8r7f7kvt8r48t5f3zdt", key0.Address);
        }
    }
}