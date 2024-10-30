namespace CryptoConnect.Adapters
{
    public class BinanceHelper
    {
        private static readonly Lazy<BinanceHelper> _instance = new Lazy<BinanceHelper>(() => new BinanceHelper());
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
                "ADAUSDT" => "Cardano",
                "DOTUSDT" => "Polkadot",
                "SOLUSDT" => "Solana",
                "DOGEUSDT" => "Dogecoin",
                "SHIBUSDT" => "Shiba Inu",
                "LINKUSDT" => "Chainlink",
                "XLMUSDT" => "Stellar",
                "UNIUSDT" => "Uniswap",
                "AVAXUSDT" => "Avalanche",
                "VETUSDT" => "VeChain",
                "TRXUSDT" => "TRON",
                "ATOMUSDT" => "Cosmos",
                "XTZUSDT" => "Tezos",
                "XMRUSDT" => "Monero",
                "ALGOUSDT" => "Algorand",
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
                "cardano" => "ADAUSDT",
                "polkadot" => "DOTUSDT",
                "solana" => "SOLUSDT",
                "dogecoin" => "DOGEUSDT",
                "shiba" => "SHIBUSDT",
                "chainlink" => "LINKUSDT",
                "stellar" => "XLMUSDT",
                "uniswap" => "UNIUSDT",
                "avalanche" => "AVAXUSDT",
                "vechain" => "VETUSDT",
                "tron" => "TRXUSDT",
                "cosmos" => "ATOMUSDT",
                "tezos" => "XTZUSDT",
                "monero" => "XMRUSDT",
                "algorand" => "ALGOUSDT",
                _ => throw new ArgumentException($"Unsupported cryptoId: {cryptoId}")
            };
        }
    }
}