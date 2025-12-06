using CryptoConnect.Interfaces;
using CryptoConnect.Models;
using Microsoft.Extensions.Logging;

namespace CryptoConnect.DataProviders
{
    public class CoinGeckoDataProvider : ICryptoDataProvider
    {
        //hardcoding the Provider Name
        public string ProviderName => "CoinGecko";
        private readonly HttpClient _httpClient;
        private readonly ICryptoDataProviderAdapter _cryptoDataProvider;
        private readonly ILogger<CoinGeckoDataProvider> _logger;

        public CoinGeckoDataProvider(IHttpClientFactory httpClientFactory, ICryptoDataProviderAdapter cryptoDataProvider, ILogger<CoinGeckoDataProvider> logger)
        {
            _httpClient = httpClientFactory.CreateClient(ProviderName);
            _cryptoDataProvider = cryptoDataProvider;
            _logger = logger;
        }

        public async Task<CryptoPrice> GetCryptoPricesAsync(string[] cryptoIds)
        {
            try
            {
                var url = $"simple/price?ids={string.Join(",", cryptoIds)}&vs_currencies=usd";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        _logger.LogWarning("CoinGecko API rate limit exceeded (429). Consider upgrading to a paid plan or reducing request frequency.");
                        throw new HttpRequestException("CoinGecko API rate limit exceeded. The free tier has strict limits. Please try again later or use a different provider.");
                    }
                    
                    _logger.LogError("CoinGecko API returned status code {StatusCode} for prices request", response.StatusCode);
                    throw new HttpRequestException($"CoinGecko API error: {response.StatusCode}");
                }
                
                var responseBody = await response.Content.ReadAsStringAsync();
                return _cryptoDataProvider.AdaptPrices(responseBody);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch crypto prices from CoinGecko for symbols: {Symbols}", string.Join(", ", cryptoIds));
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching crypto prices from CoinGecko");
                throw new Exception("Failed to fetch crypto prices from CoinGecko", ex);
            }
        }
        
        public async Task<List<CryptoMarketData>> GetCryptoMarketDatasAsync(string[] cryptoIds)
        {
            try
            {
                var url = $"coins/markets?vs_currency=usd&ids={string.Join(",", cryptoIds)}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        _logger.LogWarning("CoinGecko API rate limit exceeded (429). Consider upgrading to a paid plan or reducing request frequency.");
                        throw new HttpRequestException("CoinGecko API rate limit exceeded. The free tier has strict limits. Please try again later or use a different provider.");
                    }
                    
                    _logger.LogError("CoinGecko API returned status code {StatusCode} for market data request", response.StatusCode);
                    throw new HttpRequestException($"CoinGecko API error: {response.StatusCode}");
                }
                
                var responseBody = await response.Content.ReadAsStringAsync();
                return _cryptoDataProvider.AdaptMarketData(responseBody);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch crypto market data from CoinGecko for symbols: {Symbols}", string.Join(", ", cryptoIds));
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching crypto market data from CoinGecko");
                throw new Exception("Failed to fetch crypto market data from CoinGecko", ex);
            }
        }

    }
}