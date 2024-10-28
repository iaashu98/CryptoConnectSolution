public interface ICryptoDataProviderFactory
{
    ICryptoDataProvider GetDataProvider(string ProviderName);
}