using System;
using System.Linq;
using System.Text;
using Avalanche.Net.Api.AVMAPI;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace Tests
{
    public class Keychain
    {
        string alias = "X";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(
            "ef9bf2d4436491c153967c9709dd8e82795bdb9b5ad44ee22c2903005d1cf676",
            "033fad3644deb20d7a210d12757092312451c112d04773cee2699fbb59dc8bb2ef",
            "X-GsBFiZotn2DvHgeV4AN6htsh2k62NAb28")]
        public void ShouldGenerateKeys(string privateKey, string pubKey, string address)
        {
            var kp = new AVMKeyPair(alias,  privateKey.HexToBytes());
            var addressStr = kp.GetAddressString();

            Assert.AreEqual(pubKey,  kp.Pubk.BytesToHex());
            Assert.AreEqual(address, addressStr);
        }

        [Test]
        // TODO: Should include recoveryParam, should be equel to;
        // 03f5fccca73bf0ea0ad643133c593ed3a2a69bf9d95a0c218269c0f4a07b91fc53e29ad72a3e279c8966cccfe7ae988a94d93b23d5c5516676ac57ddab319abb01 
        [TestCase(
            "ef9bf2d4436491c153967c9709dd8e82795bdb9b5ad44ee22c2903005d1cf676",
            "03f5fccca73bf0ea0ad643133c593ed3a2a69bf9d95a0c218269c0f4a07b91fc53e29ad72a3e279c8966cccfe7ae988a94d93b23d5c5516676ac57ddab319abb")]
        [TestCase(
            "17c692d4a99d12f629d9f0ff92ec0dba15c9a83e85487b085c1a3018286995c6",
            "8ecd7d9b2613c8eabedb00333d0ae187c39a521e9631cbccf16cd14991fe5b221c9983d23583d397f3e7e377a6825c1caa5ff8b43c5863342441c48c2173119b")]
        public void ShouldSign(string privateKey, string signature)
        {
            var kp = new AVMKeyPair(alias,  privateKey.HexToBytes());
            
            var hashed = NBitcoin.Crypto.Hashes.SHA256("09090909".HexToBytes());
            var signed = kp.Sign(hashed); 

            Assert.AreEqual(64, signed.Length); // TODO: Should be 65 (with recovery param)
            Assert.AreEqual(signature.HexToBytes(), signed);
        }

        [Test]
        public void ShouldSignAndVerifyWithNewKey()
        {
            var kp = new AVMKeyPair(alias);
            
            var hashed = NBitcoin.Crypto.Hashes.SHA256("09090909".HexToBytes());
            var signed = kp.Sign(hashed); 
            Assert.IsTrue(kp.Verify(hashed, signed));
        }

        public void ShouldVerify() 
        {

        }
    }

    public static class StringExtensions
    {
        public static byte[] HexToBytes(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string BytesToHex(this byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}