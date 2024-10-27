using CryptoConnect.Models;

namespace CryptoConnect.DataProviders
{
    public class CoinGeckoDataProvider : ICryptoDataProvider
    {
        public string ProviderName => "CoinGecko";
        private readonly HttpClient _httpClient;
        private readonly ICryptoDataProviderAdapter _cryptoDataProvider;

        public CoinGeckoDataProvider(IHttpClientFactory httpClientFactory, ICryptoDataProviderAdapter cryptoDataProvider)
        {
            _httpClient = httpClientFactory.CreateClient(ProviderName);
            _cryptoDataProvider = cryptoDataProvider;
        }

        public async Task<List<CryptoMarketData>> GetCryptoMarketDatasAsync(string[] cryptoIds)
        {
            var url = $"coins/markets?vs_currency=usd&ids={string.Join(",", cryptoIds)}";
            var response = await _httpClient.GetStringAsync(url);
            return _cryptoDataProvider.AdaptMarketData(response);
        }

        public async Task<Dictionary<string, decimal>> GetCryptoPricesAsync(string[] cryptoIds)
        {
            var url = $"simple/price?ids={string.Join(",", cryptoIds)}&vs_currencies=usd";
            var response = await _httpClient.GetStringAsync(url);
            return _cryptoDataProvider.AdaptPrices(response);
        }
    }
}