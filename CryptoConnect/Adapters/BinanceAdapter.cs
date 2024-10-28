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

        // Check if the root is array or single object; observed that sometimes data is coming as an array and sometimes as an object
        if (root.ValueKind == JsonValueKind.Array)
        {
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
        
        if (root.ValueKind == JsonValueKind.Array)
        {
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
            var symbol = root.GetProperty("symbol").GetString();
            var priceString = root.GetProperty("price").GetString(); 
            var price = decimal.Parse(priceString);
            prices.Add(symbol, price);
        }
        else
        {
            Console.WriteLine("Unexpected response format from Binance API.");
        }
        return prices;
    }

}