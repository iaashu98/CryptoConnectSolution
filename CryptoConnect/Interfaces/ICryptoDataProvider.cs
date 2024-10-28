using CryptoConnect.Models;

public interface ICryptoDataProvider
{
    string ProviderName{ get; }
    Task<Dictionary<string, decimal>> GetCryptoPricesAsync(string[] cryptoIds);
    Task<List<CryptoMarketData>> GetCryptoMarketDatasAsync(string[] cryptoIds);
}