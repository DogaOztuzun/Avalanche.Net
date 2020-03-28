using System;
using System.Linq;
using Avalanche.Net.Utilities;
using NBitcoin;
using NBitcoin.BouncyCastle.Math;
using NBitcoin.Crypto;

namespace Avalanche.Net.Models.Avm
{
    public class AvmKeyPair : KeyPair
    {
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

        private ExtKey _masterKey;

        public AvmKeyPair(string chainId)
        {
            SetChainID(chainId);
            _masterKey = new ExtKey();
            this.PrivateKey = _masterKey.PrivateKey;
        }

        public AvmKeyPair(string chainId, byte[] privk) 
        {
            SetChainID(chainId);
            this.PrivateKey = new Key(privk);
        }

        public AvmKeyPair(string chainId, Mnemonic mneumonic) : this(chainId, mneumonic, "") {}
        public AvmKeyPair(string chainId, Mnemonic mneumonic, string passphrase)
        {
            SetChainID(chainId);

            byte[] seed = mneumonic.DeriveSeed(passphrase);
            _masterKey = new ExtKey(seed);
            this.PrivateKey = _masterKey.PrivateKey;
        }

        public byte[] GetAddress() 
        {
            return addressFromPublicKey();
        }

        public string GetAddressString() 
        {
            var addr = addressFromPublicKey();
            return addressToString( base._chainId, addr);
        }

        private byte[] addressFromPublicKey() 
        {
            if(this.PublicKey.ToBytes().Length == 65) 
            {
                throw new NotImplementedException();
            }

            if(this.PublicKey.ToBytes().Length == 33) 
            {
                var pubBytes = PublicKey.ToBytes();
                var sha256 =  NBitcoin.Crypto.Hashes.SHA256(pubBytes);
                var ripesha = NBitcoin.Crypto.Hashes.RIPEMD160(sha256, sha256.Length);
                return ripesha;
            }

            throw new NotSupportedException();
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

            // Create and return custom Signature model which includes v
            return new ECDSASignature(new BigInteger(1, rsigPad), new BigInteger(1, ssigPad) );
        }

        # region utils/bintools
        private string addressToString(string chainid, byte[] bytes){
            return chainid + "-" + this.avaSerialize(bytes);
        }

        private string avaSerialize(byte[] bytes) 
        {
            bytes = addChecksum(bytes);
            return Base58.Encode(bytes);
        }

        private byte[] addChecksum(byte[] buff)
        {
            var hashslice =  NBitcoin.Crypto.Hashes.SHA256(buff).Skip(28);
            return buff.Concat(hashslice).ToArray();
        }
        # endregion
    }
}