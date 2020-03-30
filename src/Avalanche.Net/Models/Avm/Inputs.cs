using System.Collections.Generic;
using Avalanche.Net.Utilities;

namespace Avalanche.Net.Models.Avm
{
    public class Input
    {
        private byte[] _txId;
        private byte[] _txIdx;
        private byte[] _assetId;
        private byte[] _inputId;

        public string GetUtxoId()
        {
            return Base58.Encode(ByteUtil.Merge(_txId, _txIdx));
        }
        
        public string GetAssetId()
        {
            return AvmKeyPair.AvaSerialize(_assetId);
        }

        public int FromBuffer(byte[] bytes, int offset = 0)
        {
            _txId = bytes.Slice(offset, offset + 32);
            offset += 32;
            _txIdx = bytes.Slice(offset, offset + 4);
            offset += 4;
            _assetId = bytes.Slice(offset, offset + 32);
            offset += 32;
            _inputId = bytes.Slice(offset, offset + 4);
            offset += 4;
            return offset;
        }
    }

    public class SecpInput : Input
    {
        private byte[] _amount;
        private byte[] _numAddr;
        private SigIdx[] _sigIdxList;
        
        public long AmountValue { get; private set; }

        public new int FromBuffer(byte[] bytes, int offset = 0)
        {
            offset = base.FromBuffer(bytes, offset);
            _amount = bytes.Slice(offset, offset + 8);
            AmountValue = _amount.ConvertToInt64();
            offset += 8;
            _numAddr = bytes.Slice(offset, offset + 4);
            offset += 4;
            var addressCount = _numAddr.ConvertToInt32();
            var sigIdxList = new List<SigIdx>();
            for (var i = 0; i < addressCount; i++)
            {
                var sigIdx = new SigIdx();
                var sigBuff = bytes.Slice(offset, offset + 4);
                sigIdx.FromBuffer(sigBuff);
                offset += 4;
                sigIdxList.Add(sigIdx);
            }
            _sigIdxList = sigIdxList.ToArray();

            return offset;
        }
    }

    public class SigIdx : NBytes
    {
        public byte[] Source;

        public SigIdx()
        {
            ByteSize = 4;
        }
    }
}
