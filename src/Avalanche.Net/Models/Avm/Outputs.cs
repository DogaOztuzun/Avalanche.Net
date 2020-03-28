using System.Collections.Generic;
using Avalanche.Net.Utilities;

namespace Avalanche.Net.Models.Avm
{
    public class Output
    {
        protected byte[] outputid;
        protected uint outputidnum;

        public int FromBuffer(byte[] buffer, int offset)
        {
            this.outputid = ByteUtil.Slice(buffer, offset, offset + 4);
            this.outputidnum = ByteUtil.ConvertToInt32(this.outputid);

            return offset + 4;
        }
    }

    public class SecpOutBase : Output
    {
        protected byte[] locktime;
        protected byte[] threshold;
        protected byte[] numaddrs;
        protected Address[] addresses;
        protected byte[] amount;
        protected long amountValue;

        public new int FromBuffer(byte[] buffer, int offset = 0)
        {
            offset = base.FromBuffer(buffer, offset);

            this.amount = ByteUtil.Slice(buffer, offset, offset + 8);
            this.amountValue = ByteUtil.ConvertToInt64(this.amount);
            offset += 8;
            this.locktime = ByteUtil.Slice(buffer, offset, offset + 8);
            offset += 8;
            this.threshold = ByteUtil.Slice(buffer, offset, offset + 4);
            offset += 4;
            this.numaddrs = ByteUtil.Slice(buffer, offset, offset + 4);
            offset += 4;

            var numaddrs = ByteUtil.ConvertToInt32(this.numaddrs);
            var addressList = new List<Address>();
            for (var i = 0; i < numaddrs; i++)
            {
                var addr = new Address();
                var offsetEnd = offset + addr.getSize();
                var copied = ByteUtil.Slice(buffer, offset, offsetEnd);
                addr.FromBuffer(copied);
                addressList.Add(addr);
                offset = offsetEnd;
            }

            this.addresses = addressList.ToArray();

            return offset;
        }
    }

    public class SecpOutput : SecpOutBase
    {
        protected byte[] assetid;

        public SecpOutput(byte[] assetid)
        {
            this.assetid = assetid;
        }

        public new int FromBuffer(byte[] buffer, int offset = 0)
        {
            this.assetid = ByteUtil.Slice(buffer, offset, offset + 32);
            offset += 32;
            offset = base.FromBuffer(buffer, offset);
            return offset;
        }
    }

    public class Address : NBytes
    {
        public Address()
        {
            this.bsize = 20;
        }
    }

    public abstract class NBytes
    {
        protected byte[] bytes;
        protected int bsize;

        public int getSize()
        {
            return bsize;
        }

        public int FromBuffer(byte[] buff)
        {
            if (buff.Length != this.bsize)
            {
                throw new System.Exception("Buffer length must be exactly " + this.bsize + " bytes.");
            }

            this.bytes = buff;

            return this.bsize;
        }
    }
}
