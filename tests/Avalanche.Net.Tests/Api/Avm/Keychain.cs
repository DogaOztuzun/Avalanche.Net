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
        public void ShouldGenerateKeys()
        {
            var kp = new AVMKeyPair(alias,  "ef9bf2d4436491c153967c9709dd8e82795bdb9b5ad44ee22c2903005d1cf676".HexToBytes());
            Assert.AreEqual("033fad3644deb20d7a210d12757092312451c112d04773cee2699fbb59dc8bb2ef",  kp.Pubk.BytesToHex());
            
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