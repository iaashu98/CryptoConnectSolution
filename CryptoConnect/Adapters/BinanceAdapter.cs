using System.Text.Json;
using CryptoConnect.Adapters;
using CryptoConnect.Interfaces;
using CryptoConnect.Models;
using Microsoft.Extensions.Logging;

public class BinanceAdapter : ICryptoDataProviderAdapter
{
    private readonly ILogger<BinanceAdapter> _logger;

    public BinanceAdapter(ILogger<BinanceAdapter> logger)
    {
        _logger = logger;
    }

    public List<CryptoMarketData> AdaptMarketData(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;

        var marketDataList = new List<CryptoMarketData>();

        // Check if the root is array or single object; observed that sometimes data is coming as an array and sometimes as an object
        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in root.EnumerateArray())
            {
                var symbol = item.GetProperty("symbol").GetString();
                if (string.IsNullOrEmpty(symbol))
                {
                    _logger.LogWarning("Skipping item with null or empty symbol");
                    continue;
                }

                var marketData = new CryptoMarketData
                {
                    Id = symbol,
                    Symbol = symbol,
                    Name = BinanceHelper.Instance.GetCryptoNameFromSymbol(symbol) ?? symbol,
                    CurrentPrice = item.TryGetProperty("lastPrice", out var lastPrice) ? Convert.ToDecimal(lastPrice.GetString()) : 0,
                    MarketCap = 0,  // Binance doesn't provide MarketCap in this API
                    Volume = item.TryGetProperty("volume", out var volume) ? Convert.ToDecimal(volume.GetString()) : 0
                };
                marketDataList.Add(marketData);
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            var symbol = root.GetProperty("symbol").GetString();
            if (!string.IsNullOrEmpty(symbol))
            {
                var marketData = new CryptoMarketData
                {
                    Id = symbol,
                    Symbol = symbol,
                    Name = BinanceHelper.Instance.GetCryptoNameFromSymbol(symbol) ?? symbol,
                    CurrentPrice = root.TryGetProperty("lastPrice", out var lastPrice) ? Convert.ToDecimal(lastPrice.GetString()) : 0,
                    MarketCap = 0,  // Binance doesn't provide MarketCap in this API
                    Volume = root.TryGetProperty("volume", out var volume) ? Convert.ToDecimal(volume.GetString()) : 0
                };
                marketDataList.Add(marketData);
            }
        }
        else
        {
            _logger.LogWarning("Unexpected response format from Binance API. ValueKind: {ValueKind}", root.ValueKind);
        }

        return marketDataList;
    }

    public CryptoPrice AdaptPrices(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;
        CryptoPrice prices = new();
        
        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in root.EnumerateArray())
            {
                var symbol = item.GetProperty("symbol").GetString();
                if (!string.IsNullOrEmpty(symbol) && item.TryGetProperty("price", out var priceElement))
                {
                    var priceString = priceElement.GetString();
                    if (decimal.TryParse(priceString, out var price))
                    {
                        prices.Prices.Add(symbol, price);
                    }
                }
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            var symbol = root.GetProperty("symbol").GetString();
            if (!string.IsNullOrEmpty(symbol) && root.TryGetProperty("price", out var priceElement))
            {
                var priceString = priceElement.GetString();
                if (decimal.TryParse(priceString, out var price))
                {
                    prices.Prices.Add(symbol, price);
                }
            }
        }
        else
        {
            _logger.LogWarning("Unexpected response format from Binance API. ValueKind: {ValueKind}", root.ValueKind);
        }
        return prices;
    }

}