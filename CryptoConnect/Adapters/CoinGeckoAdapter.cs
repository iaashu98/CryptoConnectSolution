using System.Text.Json;
using CryptoConnect.Models;

public class CoinGeckoAdapter : ICryptoDataProviderAdapter
{
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
                var marketData = new CryptoMarketData
                {
                    Id = item.GetProperty("id").GetString(),
                    Symbol = item.GetProperty("symbol").GetString(),
                    Name = item.GetProperty("name").GetString(),
                    CurrentPrice = item.GetProperty("current_price").GetDecimal(),
                    MarketCap = item.GetProperty("market_cap").GetDecimal(),
                    Volume = item.GetProperty("total_volume").GetDecimal()
                };
                marketDataList.Add(marketData);
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: one symbol
            var marketData = new CryptoMarketData
            {
                Id = root.GetProperty("id").GetString(),
                Symbol = root.GetProperty("symbol").GetString(),
                Name = root.GetProperty("name").GetString(),
                CurrentPrice = root.GetProperty("current_price").GetDecimal(),
                MarketCap = root.GetProperty("market_cap").GetDecimal(),
                Volume = root.GetProperty("volume").GetDecimal()
            };
            marketDataList.Add(marketData);
        }
        else
        {
            Console.WriteLine("Unexpected response format from CoinGecko API.");
        }

        return marketDataList;
    }

    public Dictionary<string, decimal> AdaptPrices(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;

        var prices = new Dictionary<string, decimal>();

        // Check if the root is an array (multiple symbols) or a single object (one symbol)
        if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: multiple symbols are represented as properties of the object
            foreach (var item in root.EnumerateObject())
            {
                var symbol = item.Name;  // The property name is the symbol
                var price = item.Value.GetProperty("usd").GetDecimal();  // Adjust based on CoinGecko's price format
                prices.Add(symbol, price);
            }
        }
        else
        {
            Console.WriteLine("Unexpected response format from CoinGecko API.");
        }

        return prices;
    }
}