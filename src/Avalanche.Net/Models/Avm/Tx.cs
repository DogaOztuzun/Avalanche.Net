using System.Collections.Generic;
using Avalanche.Net.Utilities;

namespace Avalanche.Net.Models.Avm
{
    public class TxUnsigned
    {
        protected byte[] txtype;
        protected byte[] networkid;
        protected byte[] blockchainid;
        protected byte[] numouts;
        protected Output[] outs;
        protected byte[] numins;
        protected Input[] ins;

        public int FromBuffer(byte[] bytes, int offset = 0)
        {
            this.txtype = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            this.networkid = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            this.blockchainid = ByteUtil.Slice(bytes, offset, offset + 32);
            offset += 32;
            this.numouts = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            var outcount = ByteUtil.ConvertToInt32(this.numouts);
            var outList = new List<Output>();
            for (var i = 0; i < outcount; i++)
            {
                var outbuff = ByteUtil.Slice(bytes, offset, bytes.Length);
                var assetid = ByteUtil.Slice(outbuff, 0, 32);
                var outputid = ByteUtil.ConvertToInt32(outbuff, 32);

                if (outputid == 4)
                {
                    var secpout = new SecpOutput(assetid);

                    offset += secpout.FromBuffer(outbuff, 0);
                    outList.Add(secpout);
                }
            }
            this.outs = outList.ToArray();

            this.numins = ByteUtil.Slice(bytes, offset, offset + 4);
            offset += 4;
            var incount = ByteUtil.ConvertToInt32(numins);
            var inList = new List<Input>();
            for (var i = 0; i < incount; i++)
            {
                var inbuff = ByteUtil.Slice(bytes, offset, bytes.Length);
                var inputid = ByteUtil.ConvertToInt32(inbuff, 68);
                if (inputid == 6)
                {
                    var input = new SecpInput();
                    offset += input.FromBuffer(inbuff);
                    inList.Add(input);
                }
            }
            this.ins = inList.ToArray();

            return offset;
        }
    }

    public class Tx
    {
        protected TxUnsigned tx;
        protected object[][] signatures;
    }
}
