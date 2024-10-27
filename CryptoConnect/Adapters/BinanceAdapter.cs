using System.Text.Json;
using CryptoConnect.Adapters;
using CryptoConnect.Models;

public class BinanceAdapter : ICryptoDataProviderAdapter
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
                    Id = item.GetProperty("symbol").GetString(),
                    Symbol = item.GetProperty("symbol").GetString(),
                    Name = BinanceHelper.Instance.GetCryptoNameFromSymbol(item.GetProperty("symbol").GetString()),
                    CurrentPrice = Convert.ToDecimal(item.GetProperty("lastPrice").GetString()),
                    MarketCap = 0,  // Binance doesn't provide MarketCap in this API
                    Volume = Convert.ToDecimal(item.GetProperty("volume").GetString())
                };
                marketDataList.Add(marketData);
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: one symbol
            var marketData = new CryptoMarketData
            {
                Id = root.GetProperty("symbol").GetString(),
                Symbol = root.GetProperty("symbol").GetString(),
                Name = BinanceHelper.Instance.GetCryptoNameFromSymbol(root.GetProperty("symbol").GetString()),
                CurrentPrice = Convert.ToDecimal(root.GetProperty("lastPrice").GetString()),
                MarketCap = 0,  // Binance doesn't provide MarketCap in this API
                Volume = Convert.ToDecimal(root.GetProperty("volume").GetString())
            };
            marketDataList.Add(marketData);
        }
        else
        {
            Console.WriteLine("Unexpected response format from Binance API.");
        }

        return marketDataList;
    }

    public Dictionary<string, decimal> AdaptPrices(string rawData)
    {
        using var jsonDocument = JsonDocument.Parse(rawData);
        var root = jsonDocument.RootElement;

        var prices = new Dictionary<string, decimal>();

        Console.WriteLine("Kind of root: "+ root.ValueKind);
        // Check if the root is an array (multiple symbols) or a single object (one symbol)
        if (root.ValueKind == JsonValueKind.Array)
        {
            // Handle the array case: multiple symbols
            foreach (var item in root.EnumerateArray())
            {
                var symbol = item.GetProperty("symbol").GetString();
                var priceString = item.GetProperty("price").GetString(); 
                var price = decimal.Parse(priceString);
                prices.Add(symbol, price);
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            // Handle the object case: one symbol
            var symbol = root.GetProperty("symbol").GetString();
            var priceString = root.GetProperty("price").GetString(); 
            var price = decimal.Parse(priceString);
            prices.Add(symbol, price);
        }
        else
        {
            Console.WriteLine("Unexpected response format from Binance API.");
        }
        System.Console.WriteLine(prices.Select(x => "Price:" + x.Key ));
        return prices;
    }

}