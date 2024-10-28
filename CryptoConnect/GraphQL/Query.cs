using CryptoConnect.Models;

namespace CryptoConnect.GraphQL
{
    public class Query
    {
        private readonly ICryptoDataProviderFactory _cryptoDataProviderFactory;
        public Query(ICryptoDataProviderFactory cryptoDataProviderFactory)
        {
            _cryptoDataProviderFactory = cryptoDataProviderFactory;
        }

        [GraphQLName("cryptoPrices")]
        public async Task<CryptoPrice> GetCryptoPrices(string[] cryptoIds, string provider)
        {
            var dataProvider = _cryptoDataProviderFactory.GetDataProvider(provider);
            return await dataProvider.GetCryptoPricesAsync(cryptoIds);
        }

        [GraphQLName("cryptoMarketData")]
        public async Task<List<CryptoMarketData>> GetCryptoMarketDatas(string[] cryptoIds, string provider)
        {
            var dataProvider = _cryptoDataProviderFactory.GetDataProvider(provider);
            return await dataProvider.GetCryptoMarketDatasAsync(cryptoIds);
        }
    }
}