using CryptoConnect.Models;

namespace CryptoConnect.GraphQL
{
    public class CryptoMarketDataType : ObjectType<CryptoMarketData>
    {
        protected override void Configure(IObjectTypeDescriptor<CryptoMarketData> descriptor)
        {
            descriptor.Field(f => f.Id).Type<StringType>();
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Symbol).Type<StringType>();
            descriptor.Field(f => f.CurrentPrice).Type<StringType>();
            descriptor.Field(f => f.MarketCap).Type<StringType>();
            descriptor.Field(f => f.Volume).Type<StringType>();
        }
    
    }
}