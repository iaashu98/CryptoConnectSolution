using System.Text.Json;
using CryptoConnect.Interfaces;
using CryptoConnect.Models;
using Microsoft.Extensions.Logging;

public class CoinGeckoAdapter : ICryptoDataProviderAdapter
{
    private readonly ILogger<CoinGeckoAdapter> _logger;

    public CoinGeckoAdapter(ILogger<CoinGeckoAdapter> logger)
    {
        _logger = logger;
    }

    public List<CryptoMarketData> AdaptMarketData(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;

        var marketDataList = new List<CryptoMarketData>();

        // Check if the root is an array (multiple symbols) or a single object (one symbol)
        if (root.ValueKind == JsonValueKind.Array)
        {
            // Handle the array case: multiple symbols
            foreach (var item in root.EnumerateArray())
            {
                var id = item.GetProperty("id").GetString();
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogWarning("Skipping item with null or empty id");
                    continue;
                }

                var marketData = new CryptoMarketData
                {
                    Id = id,
                    Symbol = item.TryGetProperty("symbol", out var symbol) ? symbol.GetString() ?? id : id,
                    Name = item.TryGetProperty("name", out var name) ? name.GetString() ?? id : id,
                    CurrentPrice = item.TryGetProperty("current_price", out var currentPrice) ? currentPrice.GetDecimal() : 0,
                    MarketCap = item.TryGetProperty("market_cap", out var marketCap) ? marketCap.GetDecimal() : 0,
                    Volume = item.TryGetProperty("total_volume", out var totalVolume) ? totalVolume.GetDecimal() : 0
                };
                marketDataList.Add(marketData);
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: one symbol
            var id = root.GetProperty("id").GetString();
            if (!string.IsNullOrEmpty(id))
            {
                var marketData = new CryptoMarketData
                {
                    Id = id,
                    Symbol = root.TryGetProperty("symbol", out var symbol) ? symbol.GetString() ?? id : id,
                    Name = root.TryGetProperty("name", out var name) ? name.GetString() ?? id : id,
                    CurrentPrice = root.TryGetProperty("current_price", out var currentPrice) ? currentPrice.GetDecimal() : 0,
                    MarketCap = root.TryGetProperty("market_cap", out var marketCap) ? marketCap.GetDecimal() : 0,
                    Volume = root.TryGetProperty("volume", out var volume) ? volume.GetDecimal() : 0
                };
                marketDataList.Add(marketData);
            }
        }
        else
        {
            _logger.LogWarning("Unexpected response format from CoinGecko API. ValueKind: {ValueKind}", root.ValueKind);
        }

        return marketDataList;
    }

    public CryptoPrice AdaptPrices(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;

        CryptoPrice prices = new();

        // Check if the root is an array (multiple symbols) or a single object (one symbol)
        if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: multiple symbols are represented as properties of the object
            foreach (var item in root.EnumerateObject())
            {
                var symbolName = item.Name;  // The property name is the symbol
                if (!string.IsNullOrEmpty(symbolName) && item.Value.TryGetProperty("usd", out var usdPrice))
                {
                    var price = usdPrice.GetDecimal();
                    prices.Prices.Add(symbolName, price);
                }
            }
        }
        else
        {
            _logger.LogWarning("Unexpected response format from CoinGecko API. ValueKind: {ValueKind}", root.ValueKind);
        }

        return prices;
    }
}