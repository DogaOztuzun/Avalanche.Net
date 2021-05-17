using Avalanche.Net.Models.Avm;
using Avalanche.Net.Utilities;
using NBitcoin;
using NUnit.Framework;

namespace Avalanche.Net.Tests.Api.Avm
{
    public class Keychain
    {
        string alias = "X";

        [Test]
        [TestCase(
            "ef9bf2d4436491c153967c9709dd8e82795bdb9b5ad44ee22c2903005d1cf676",
            "X-avax14crnapsnw2rpx8d430yrnnw70ctkup6gpzsnk2")]
        [TestCase(
            "17c692d4a99d12f629d9f0ff92ec0dba15c9a83e85487b085c1a3018286995c6",
            "X-avax1v2mhfuk9tryzkg6983fsd460gv0wkv5kf3nfxn")]
        [TestCase(
            "d0e17d4b31380f96a42b3e9ffc4c1b2a93589a1e51d86d7edc107f602fbc7475",
            "X-avax15h5rxr8arrdr27zu872tqks7scuy7q6rtjhl6x")]
        public void ShouldGenerateKeys(string privateKey, string address)
        {
            var kp = new AvmKeyPair(alias,  privateKey);
            var addressStr = kp.GetAddressString();

            Assert.AreEqual(address, addressStr);
        }

        [Test]
        public void ShouldGenerateKeysWithMnemonic()
        {
            var mnemonic = new Mnemonic("clever glove portion swing nerve bullet boil rose motion nose rocket tube color account enhance");
            var kp = new AvmKeyPair(alias, mnemonic);
            var addressStr = kp.GetAddressString();

            Assert.AreEqual("0343087424849a4d3ca79a0eb89e14e00ff8a8c042e4f5f891f3d57c88737bac67",  kp.Pubk.BytesToHex());
            Assert.AreEqual("X-avax1pzw2dc55hckyn68dwfmtcqdpvltc3s87q4gve8", addressStr);
        }

        [Test]
        public void ShouldGenerateKeysWithMnemonicAndPassword()
        {
            var mnemonic = new Mnemonic("clever glove portion swing nerve bullet boil rose motion nose rocket tube color account enhance");
            var kp = new AvmKeyPair(alias, mnemonic, "P@aaw0rd");
            var addressStr = kp.GetAddressString();

            Assert.AreEqual("020ea542208ffa2bef2213bb1b3460e95d0134056bb211317c323a1d102cf856b5",  kp.Pubk.BytesToHex());
            Assert.AreEqual("X-avax1908hms500kcnht8msg3rr0qn0qt9qmc78m7se0", addressStr);
        }

        // [TestCase(
        //     "ef9bf2d4436491c153967c9709dd8e82795bdb9b5ad44ee22c2903005d1cf676",
        //     "03f5fccca73bf0ea0ad643133c593ed3a2a69bf9d95a0c218269c0f4a07b91fc53e29ad72a3e279c8966cccfe7ae988a94d93b23d5c5516676ac57ddab319abb01")]
        // [TestCase(
        //     "17c692d4a99d12f629d9f0ff92ec0dba15c9a83e85487b085c1a3018286995c6",
        //     "8ecd7d9b2613c8eabedb00333d0ae187c39a521e9631cbccf16cd14991fe5b221c9983d23583d397f3e7e377a6825c1caa5ff8b43c5863342441c48c2173119b01")]
        // [TestCase(
        //     "d0e17d4b31380f96a42b3e9ffc4c1b2a93589a1e51d86d7edc107f602fbc7475",
        //     "be9fb13be75791778ea1081820f9d25d808d0714a92880a7144a0f9809961b43079576349b255399f5339550db7f5fec4b989e3364a5ffb0aca09e5ad72e92af00")]
        [Ignore("Verification not implemented.")]
        public void ShouldSign(string privateKey, string signature)
        {
            var kp = new AvmKeyPair(alias,  privateKey);
            
            var hashed = NBitcoin.Crypto.Hashes.SHA256("09090909".HexToBytes());
            var signed = kp.Sign(hashed); 

            Assert.AreEqual(65, signed.Length); 
            Assert.AreEqual(signature.HexToBytes(), signed);
            Assert.IsTrue(kp.Verify(hashed, signed));
        }
    }
}