namespace Avalanche.Net.Models.Avm
{
    public abstract class KeyPair
    {
        public byte[] Pubk { get; set;}
        public byte[] Privk { get; set;}

        protected string _chainId { get; set;}
        
        protected string GetChainID() 
        {
            return this._chainId;
        }

        protected void SetChainID(string chainid)
        {
            this._chainId = chainid;
        }
    }
}