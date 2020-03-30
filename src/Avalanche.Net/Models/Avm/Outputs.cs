using System;
using System.Collections.Generic;
using System.Linq;
using Avalanche.Net.Utilities;

namespace Avalanche.Net.Models.Avm
{
    public class Output
    {
        private byte[] _outputId;
        private uint _outputIdCount;

        public int FromBuffer(byte[] buffer, int offset)
        {
            _outputId = buffer.Slice(offset, offset + 4);
            _outputIdCount = _outputId.ConvertToInt32();

            return offset + 4;
        }
    }

    public class SecpOutBase : Output
    {
        private byte[] _lockTime;
        private byte[] _threshold;
        private byte[] _addressCount;
        private Address[] _addressList;
        private byte[] _amount;
        public long AmountValue { get; private set; }

        public List<string> GetAddresses()
        {
            return _addressList.Select(x => AvmKeyPair.AvaSerialize(x.ToBuffer())).ToList();
        }

        protected new int FromBuffer(byte[] buffer, int offset = 0)
        {
            offset = base.FromBuffer(buffer, offset);

            _amount = buffer.Slice(offset, offset + 8);
            AmountValue = _amount.ConvertToInt64();
            offset += 8;
            _lockTime = buffer.Slice(offset, offset + 8);
            offset += 8;
            _threshold = buffer.Slice(offset, offset + 4);
            offset += 4;
            _addressCount = buffer.Slice(offset, offset + 4);
            offset += 4;

            var addressCount = _addressCount.ConvertToInt32();
            var addressList = new List<Address>();
            for (var i = 0; i < addressCount; i++)
            {
                var addr = new Address();
                var offsetEnd = offset + addr.GetSize();
                var copied = buffer.Slice(offset, offsetEnd);
                addr.FromBuffer(copied);
                addressList.Add(addr);
                offset = offsetEnd;
            }

            _addressList = addressList.ToArray();

            return offset;
        }
    }

    public class SecpOutput : SecpOutBase
    {
        private byte[] _assetId;

        public string GetAssetId()
        {
            return AvmKeyPair.AvaSerialize(_assetId);
        }

        public SecpOutput(byte[] assetId)
        {
            _assetId = assetId;
        }

        public new int FromBuffer(byte[] buffer, int offset = 0)
        {
            _assetId = buffer.Slice(offset, offset + 32);
            offset += 32;
            offset = base.FromBuffer(buffer, offset);
            return offset;
        }
    }

    public class Address : NBytes
    {
        public byte[] ToBuffer()
        {
            return Bytes;
        }
        
        public Address()
        {
            ByteSize = 20;
        }
    }

    public abstract class NBytes
    {
        protected byte[] Bytes;
        protected int ByteSize;

        public int GetSize()
        {
            return ByteSize;
        }

        public int FromBuffer(byte[] buff)
        {
            if (buff.Length != ByteSize)
            {
                throw new Exception("Buffer length must be exactly " + ByteSize + " bytes.");
            }

            Bytes = buff;

            return ByteSize;
        }
    }
}
