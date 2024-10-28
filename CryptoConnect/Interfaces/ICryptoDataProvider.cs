using CryptoConnect.Models;

public interface ICryptoDataProvider
{
    string ProviderName{ get; }
    Task<CryptoPrice> GetCryptoPricesAsync(string[] cryptoIds);
    Task<List<CryptoMarketData>> GetCryptoMarketDatasAsync(string[] cryptoIds);
}