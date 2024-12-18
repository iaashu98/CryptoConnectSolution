using CryptoConnect.Models;
using CryptoConnect.Adapters;
using System.Text.Json;
using CryptoConnect.Interfaces;

namespace CryptoConnect.DataProviders
{
    public class BinanceDataProvider : ICryptoDataProvider
    {
        //hardcoding the provider name
        public string ProviderName => "Binance";
        public readonly HttpClient _httpClient;
        public readonly ICryptoDataProviderAdapter _cryptoDataProvider;

        public BinanceDataProvider(IHttpClientFactory httpClientFactory, ICryptoDataProviderAdapter cryptoDataProvider)
        {
            _httpClient = httpClientFactory.CreateClient(ProviderName);
            _cryptoDataProvider = cryptoDataProvider;
        }

        public async Task<List<CryptoMarketData>> GetCryptoMarketDatasAsync(string[] cryptoIds)
        {
            var tasks = new List<Task<CryptoMarketData>>();

            foreach (var id in cryptoIds)
            {
                tasks.Add(GetMarketDataForSymbolAsync(id));
            }
            var marketDataArray = await Task.WhenAll(tasks);
            return marketDataArray.ToList();
        }

        private async Task<CryptoMarketData> GetMarketDataForSymbolAsync(string cryptoId)
        {
            try
            {
                var symbol = BinanceHelper.Instance.GetBinanceSymbol(cryptoId);
                var url = $"api/v3/ticker/24hr?symbol={symbol}";
                var response = await _httpClient.GetStringAsync(url); 
                var marketData = _cryptoDataProvider.AdaptMarketData(response);

                if (marketData == null || !marketData.Any())
                {
                    throw new Exception($"No market data returned for symbol {symbol}");
                }

                return marketData.First();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to fetch market data for symbol {cryptoId} from Binance provider; Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching market data for symbol {cryptoId}; Message: {ex.Message}");
            }
        }

        public async Task<CryptoPrice> GetCryptoPricesAsync(string[] cryptoIds)
        {
            try
            {
                CryptoPrice prices = new();
                var binanceSymbols = cryptoIds.Select(x => BinanceHelper.Instance.GetBinanceSymbol(x)).ToList();

                var response = await _httpClient.GetStringAsync("api/v3/ticker/price");

                using var jsonDocument = JsonDocument.Parse(response);
                var root = jsonDocument.RootElement;

                var filteredElements = root.EnumerateArray().Where(
                    x =>
                    {
                        var symbol = x.GetProperty("symbol").GetString().ToLower();
                        return binanceSymbols.Contains(symbol, StringComparer.OrdinalIgnoreCase);
                    }
                ).ToList();

                var filteredResponse = JsonSerializer.Serialize(filteredElements);
                prices = _cryptoDataProvider.AdaptPrices(filteredResponse);

                return prices;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to fetch data from Binance provider", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching crypto prices", ex);
            }
        }
    }
}