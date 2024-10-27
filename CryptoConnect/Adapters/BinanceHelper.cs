namespace CryptoConnect.Adapters
{
    public class BinanceHelper 
    {
        private static readonly Lazy<BinanceHelper> _instance =   new Lazy<BinanceHelper>(() => new BinanceHelper());

        private BinanceHelper() { }   

        public static BinanceHelper Instance => _instance.Value;

        public string GetCryptoNameFromSymbol(string symbol)
        {
            return symbol switch
            {
                "BTCUSDT" => "Bitcoin",
                "ETHUSDT" => "Ethereum",
                "XRPUSDT" => "Ripple",
                "LTCUSDT" => "Litecoin",
                _ => "Unknown"
            };
        }

        public string GetBinanceSymbol(string cryptoId)
        {
            return cryptoId.ToLower() switch
            {
                "bitcoin" => "BTCUSDT",
                "ethereum" => "ETHUSDT",
                "ripple" => "XRPUSDT",
                "litecoin" => "LTCUSDT",
                _ => throw new ArgumentException($"Unsupported cryptoId: {cryptoId}")
            };
        }
    }
}