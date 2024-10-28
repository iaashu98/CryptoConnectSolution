using CryptoConnect.Models;

namespace CryptoConnect.Interfaces
{
    public interface ICryptoDataProviderAdapter
    {
        CryptoPrice AdaptPrices(string rawData);
        List<CryptoMarketData> AdaptMarketData(string rawData);
    }
}