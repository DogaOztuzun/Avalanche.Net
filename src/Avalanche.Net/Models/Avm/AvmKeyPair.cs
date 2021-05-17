using System;
using System.Linq;
using Avalanche.Net.Utilities;
using HDWallet.Avalanche;
using HDWallet.Core;
using NBitcoin;
using NBitcoin.BouncyCastle.Math;
using NBitcoin.Crypto;

namespace Avalanche.Net.Models.Avm
{
    public class AvmKeyPair : KeyPair
    {
        public AvalancheWallet Wallet { get; }

        private Key _privateKey;
        public Key PrivateKey { 
            get {
                return _privateKey;
            }
             private set {
                 _privateKey = value;
                 this.PublicKey = this.PrivateKey.PubKey;
             }
        }
        
        private PubKey _publicKey;
        public PubKey PublicKey { 
            get {
                return _publicKey;
            } 
            private set {
                _publicKey = value;
                this.Pubk = this.PublicKey.ToBytes();
            }
        }


        public AvmKeyPair(string chainId, string privk) 
        {
            this.Wallet = new AvalancheWallet(privk);

            SetChainID(chainId);
            this.PrivateKey = this.Wallet.PrivateKey;
        }

        public AvmKeyPair(string chainId, Mnemonic mneumonic) : this(chainId, mneumonic, "") {}
        public AvmKeyPair(string chainId, Mnemonic mneumonic, string passphrase): this(chainId, string.Join(" ", mneumonic.Words), passphrase) {}
        public AvmKeyPair(string chainId, string words, string passphrase)
        {
            SetChainID(chainId);

            IHDWallet<AvalancheWallet> hdWallet = new AvalancheHDWallet(words, passphrase);
            this.Wallet = hdWallet.GetMasterWallet();
            
            this.PrivateKey = this.Wallet.PrivateKey;
        }

        public string GetAddressString() 
        {
            return this.Wallet.Address;
        }
        
        private string addressToString(string chainid, byte[] bytes){
            return chainid + "-" + AvaSerialize(bytes);
        }

        public byte[] Sign(byte[] msg) 
        { 
            // Returns signature as v + r + s
            var signature = PrivateKey.SignCompact(new uint256(msg), forceLowR: false);

            // NBitcoin adds 27 to recovery parameter
            var recId = signature[0];            
            int headerByte = recId - 27 - (this.PrivateKey.IsCompressed ? 4 : 0);
            
            // Convert from v + r + s -> r + s + v
            var vsigPad = new byte[] { (byte) headerByte};
            var rsigPad = new byte[32];
            var ssigPad = new byte[32];

            Array.Copy(signature, 1, rsigPad, 0, 32);
            Array.Copy(signature, 33, ssigPad, 0, 32);

            return To64ByteArray(rsigPad,ssigPad, vsigPad);
        }

        private byte[] To64ByteArray(byte[] r, byte[] s, byte[] v)
        {
            var rsigPad = new byte[32];
            var ssigPad = new byte[32];
            var vsigPad = new byte[1];

            Array.Copy(r, 0, rsigPad, rsigPad.Length - r.Length, r.Length);
            Array.Copy(s, 0, ssigPad, ssigPad.Length - s.Length, s.Length);
            Array.Copy(v, 0, vsigPad, vsigPad.Length - v.Length, v.Length);

            return ByteUtil.Merge(rsigPad, ssigPad, vsigPad);
        }
        
        public bool Verify(byte[] msg, byte[] sig) 
        {
            var signature = sigFromSigBuffer(sig);
            return PublicKey.Verify(new uint256(msg), signature);
        }

        private ECDSASignature sigFromSigBuffer(byte[] sig)
        {
            // TODO: Get v from signature
            var rsigPad = new byte[32];
            var ssigPad = new byte[32];

            Array.Copy(sig, 0, rsigPad, 0, 32);
            Array.Copy(sig, 32, ssigPad, 0, 32);

            throw new NotImplementedException();

            // TODO: Create and return custom Signature model which includes v
            // TODO: Need to find a way with latest version of NBitcoin
            // return new ECDSASignature(new BigInteger(1, rsigPad), new BigInteger(1, ssigPad) );
        }

        # region utils/bintools

        public static string AvaSerialize(byte[] bytes) 
        {
            bytes = AddChecksum(bytes);
            return Base58.Encode(bytes);
        }

        private static byte[] AddChecksum(byte[] buff)
        {
            var hashslice =  NBitcoin.Crypto.Hashes.SHA256(buff).Skip(28);
            return buff.Concat(hashslice).ToArray();
        }
        # endregion
    }
}