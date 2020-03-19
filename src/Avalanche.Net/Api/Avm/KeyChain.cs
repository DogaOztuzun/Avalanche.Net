using System;
using System.Linq;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;

namespace Avalanche.Net.Api.AVMAPI
{
    public class AVMKeyPair : KeyPair
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

        public AVMKeyPair(string chainId)
        {
            SetChainID(chainId);
            _masterKey = new ExtKey();
            this.PrivateKey = _masterKey.PrivateKey;
        }

        public AVMKeyPair(string chainId, byte[] privk) 
        {
            SetChainID(chainId);
            this.PrivateKey = new Key(privk);
        }

        public AVMKeyPair(string chainId, Mnemonic mneumonic) : this(chainId, mneumonic, "") {}
        public AVMKeyPair(string chainId, Mnemonic mneumonic, string passphrase)
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
            ECDSASignature signature = PrivateKey.Sign(new uint256(msg), useLowR: false);
            byte[] r = signature.R.ToByteArrayUnsigned();
            byte[] s = signature.S.ToByteArrayUnsigned();

            // TODO: Add recoveryParam

            return To64ByteArray(r,s);
        }

        private byte[] To64ByteArray(byte[] r, byte[] s)
        {
            var rsigPad = new byte[32];
            Array.Copy(r, 0, rsigPad, rsigPad.Length - r.Length, r.Length);

            var ssigPad = new byte[32];
            Array.Copy(s, 0, ssigPad, ssigPad.Length - s.Length, s.Length);

            return ByteUtil.Merge(rsigPad, ssigPad);
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