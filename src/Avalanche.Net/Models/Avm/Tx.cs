using System.Collections.Generic;
using Avalanche.Net.Utilities;
using NBitcoin.Crypto;

namespace Avalanche.Net.Models.Avm
{
    public class TxUnsigned
    {
        private byte[] _txType;
        private byte[] _networkId;
        private byte[] _blockChainId;
        private byte[] _outCount;
        private Output[] _outs;
        private byte[] _inCount;
        private Input[] _ins;
        
        public uint GetNetworkId()
        {
            return _networkId.ConvertToInt32();
        }
        
        public string GetBlockChainId()
        {
            return AvmKeyPair.AvaSerialize(_blockChainId);
        }

        public Input[] GetInputs()
        {
            return _ins;
        }

        public Output[] GetOutputs()
        {
            return _outs;
        }

        public int FromBuffer(byte[] bytes, int offset = 0)
        {
            _txType = bytes.Slice(offset, offset + 4);
            offset += 4;
            _networkId = bytes.Slice(offset, offset + 4);
            offset += 4;
            _blockChainId = bytes.Slice(offset, offset + 32);
            offset += 32;
            _outCount = bytes.Slice(offset, offset + 4);
            offset += 4;
            var outCount = this._outCount.ConvertToInt32();
            var outList = new List<Output>();
            for (var i = 0; i < outCount; i++)
            {
                var outBuff = bytes.Slice(offset, bytes.Length);
                var assetId = outBuff.Slice(0, 32);
                var outputId = outBuff.ConvertToInt32(32);

                if (outputId == 4)
                {
                    var secpOutput = new SecpOutput(assetId);

                    offset += secpOutput.FromBuffer(outBuff);
                    outList.Add(secpOutput);
                }
            }
            _outs = outList.ToArray();

            _inCount = bytes.Slice(offset, offset + 4);
            offset += 4;
            var inCount = _inCount.ConvertToInt32();
            var inList = new List<Input>();
            for (var i = 0; i < inCount; i++)
            {
                var inBuff = bytes.Slice(offset, bytes.Length);
                var inputId = inBuff.ConvertToInt32(68);
                if (inputId == 6)
                {
                    var input = new SecpInput();
                    offset += input.FromBuffer(inBuff);
                    inList.Add(input);
                }
            }
            _ins = inList.ToArray();

            return offset;
        }
    }

    public class Tx
    {
        public TxUnsigned TxUnsigned { get; private set; }
        protected object[][] Signatures;
        public string TxId { get; private set; }

        public int FromBuffer(byte[] bytes, int offset = 0)
        {
            TxUnsigned = new TxUnsigned();
            offset = TxUnsigned.FromBuffer(bytes, offset);
            TxId = AvmKeyPair.AvaSerialize( Hashes.SHA256(bytes));

            return offset;
        }
    }
}
