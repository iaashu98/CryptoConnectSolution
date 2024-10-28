namespace CryptoConnect.Factories
{
    public class CryptoDataProviderFactory : ICryptoDataProviderFactory
    {
        private readonly IDictionary<string, ICryptoDataProvider> _providers;
        public CryptoDataProviderFactory(IEnumerable<ICryptoDataProvider> providers)
        {
            _providers = providers.ToDictionary(x => x.ProviderName, StringComparer.OrdinalIgnoreCase);
        }
        
        public ICryptoDataProvider GetDataProvider(string ProviderName)
        {
            if (_providers.TryGetValue(ProviderName, out var provider))
            {
                return provider;
            }
            throw new ArgumentException($"Provider '{ProviderName}' not found.");
        }
    }
}