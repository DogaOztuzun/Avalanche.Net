using System.Collections.Generic;
using Avalanche.Net.Utilities;

namespace Avalanche.Net.Models.Avm
{
    public class Input
    {
        protected byte[] txid;
        protected byte[] txidx;
        protected byte[] assetid;
        protected byte[] inputid;

        public int FromBuffer(byte[] bytes, int offset = 0)
        {
            this.txid = ByteUtil.Slice(bytes, offset, offset + 32);
            offset += 32;
            this.txidx = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            this.assetid = ByteUtil.Slice(bytes, offset, offset + 32);
            offset += 32;
            this.inputid = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            return offset;
        }
    }

    public class SecpInput : Input
    {
        protected byte[] amount;
        protected long amountValue;
        protected byte[] numAddr;
        protected SigIdx[] sigIdxs;

        public new int FromBuffer(byte[] bytes, int offset = 0)
        {
            offset = base.FromBuffer(bytes, offset);
            this.amount = ByteUtil.Slice(bytes, offset, offset + 8);
            this.amountValue = ByteUtil.ConvertToInt64(this.amount);
            offset += 8;
            this.numAddr = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            var numaddr = ByteUtil.ConvertToInt32(this.numAddr);
            var sigIdxList = new List<SigIdx>();
            for (var i = 0; i < numaddr; i++)
            {
                var sigidx = new SigIdx();
                var sigbuff = ByteUtil.Slice(bytes, offset, offset + 4);
                sigidx.FromBuffer(sigbuff);
                offset += 4;
                sigIdxList.Add(sigidx);
            }
            this.sigIdxs = sigIdxList.ToArray();

            return offset;
        }
    }

    public class SigIdx : NBytes
    {
        public byte[] Source;

        public SigIdx()
        {
            this.bsize = 4;
        }
    }
}
