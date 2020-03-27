namespace Avalanche.Net.Api.AVMAPI
{
    public abstract class KeyPair
    {
        public byte[] Pubk { get; set;}
        public byte[] Privk { get; set;}

        protected string _chainId { get; set;}
        
        public string GetChainID() 
        {
            return this._chainId;
        }

        public void SetChainID(string chainid)
        {
            this._chainId = chainid;
        }
    }
}