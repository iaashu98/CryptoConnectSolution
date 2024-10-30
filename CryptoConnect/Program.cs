using CryptoConnect.DataProviders;
using CryptoConnect.Factories;
using CryptoConnect.GraphQL;
using CryptoConnect.Interfaces;
using CryptoConnect.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("CoinGecko", client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
    client.DefaultRequestHeaders.Add("User-Agent", "CryptoMicroservice/1.0");
});

builder.Services.AddHttpClient("Binance", client =>
{
    client.BaseAddress = new Uri("https://api.binance.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "CryptoMicroservice/1.0");
});

builder.Services.AddSingleton<ICryptoDataProviderAdapter, CoinGeckoAdapter>();
builder.Services.AddSingleton<ICryptoDataProviderAdapter, BinanceAdapter>();

builder.Services.AddSingleton<ICryptoDataProvider>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var adapters = serviceProvider.GetServices<ICryptoDataProviderAdapter>();
    var coinGeckoAdapter = adapters.First(a => a is CoinGeckoAdapter);
    return new CoinGeckoDataProvider(httpClientFactory, coinGeckoAdapter);
});

builder.Services.AddSingleton<ICryptoDataProvider>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var adapters = serviceProvider.GetServices<ICryptoDataProviderAdapter>();
    var binanceAdapter = adapters.First(a => a is BinanceAdapter);
    return new BinanceDataProvider(httpClientFactory, binanceAdapter);
});

builder.Services.AddSingleton<ICryptoDataProviderFactory, CryptoDataProviderFactory>();

builder.Services
       .AddGraphQLServer()
       .AddQueryType<Query>()
       .AddType<CryptoMarketData>()
       .AddType<CryptoPrice>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    builder =>
            builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithOrigins("http://localhost:5173"));
});

var app = builder.Build();

app.UseCors("AllowReactApp");
app.MapGraphQL("/graphql");

app.Run();
