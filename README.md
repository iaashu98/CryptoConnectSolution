# CryptoConnect Solution

A production-ready, full-stack cryptocurrency dashboard built with .NET 8 and React, featuring real-time market data from multiple providers (Binance and CoinGecko). The application demonstrates enterprise-grade architecture with comprehensive error handling, logging, and extensible design patterns.

## Table of Contents
- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Design Patterns](#design-patterns)
- [Error Handling](#error-handling)
- [Troubleshooting](#troubleshooting)
- [Production Considerations](#production-considerations)
- [Future Enhancements](#future-enhancements)
- [Contributing](#contributing)

## Overview

CryptoConnect is a sophisticated cryptocurrency market data aggregator that provides a unified interface to multiple crypto data providers. Built with scalability and maintainability in mind, it leverages GraphQL for efficient data querying and implements industry-standard design patterns for clean, extensible code.

### Screenshots

#### Single Cryptocurrency Selection
<img width="1437" alt="Coingecko provider" src="https://github.com/user-attachments/assets/f15944de-d7f6-47af-8dfd-6d98be6ad854">
<img width="1433" alt="Binance provider" src="https://github.com/user-attachments/assets/8a52a10a-6d56-4b38-94e2-81a43bc55602">

#### Multiple Cryptocurrency Selection
<img width="1432" alt="Binance provider" src="https://github.com/user-attachments/assets/f5afe899-16e9-4678-9f3b-66136e128d3b">
<img width="1434" alt="Coingecko provider"  src="https://github.com/user-attachments/assets/4282c002-452a-419c-8866-20a00ed38f0a">

## Key Features

### Core Functionality
- ✅ **Real-Time Market Data** - Live cryptocurrency prices, market cap, and trading volume
- ✅ **Multi-Provider Support** - Seamlessly switch between Binance and CoinGecko APIs
- ✅ **GraphQL API** - Efficient, flexible data querying with Hot Chocolate
- ✅ **Responsive UI** - Modern, mobile-friendly interface built with React and Tailwind CSS
- ✅ **Type Safety** - Full TypeScript support on frontend, C# on backend

### Advanced Features
- ✅ **Dynamic Provider Selection** - Choose data source at query time
- ✅ **Standardized Data Format** - Consistent response structure regardless of provider
- ✅ **Comprehensive Error Handling** - User-friendly error messages with detailed logging
- ✅ **Rate Limit Management** - Graceful handling of API rate limits
- ✅ **Null Safety** - Robust null checking to prevent runtime errors
- ✅ **Environment-Based Configuration** - Easy deployment across different environments
- ✅ **Concurrent API Calls** - Optimized performance with Promise.all
- ✅ **CI/CD Pipeline** - Automated testing with GitHub Actions

## Architecture

### High-Level Architecture

```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│   React UI      │────────▶│  GraphQL API     │────────▶│  Data Providers │
│  (TypeScript)   │  HTTP   │  (.NET 8)        │         │   (Adapters)    │
└─────────────────┘         └──────────────────┘         └─────────────────┘
                                     │                             │
                                     │                             ▼
                                     │                    ┌─────────────────┐
                                     │                    │  External APIs  │
                                     ▼                    │  - Binance      │
                            ┌──────────────────┐         │  - CoinGecko    │
                            │  Error Handler   │         └─────────────────┘
                            │  & Logger        │
                            └──────────────────┘
```

### Backend Architecture

The backend follows a layered architecture with clear separation of concerns:

1. **GraphQL Layer** (`Query.cs`) - Handles incoming GraphQL queries
2. **Factory Layer** (`CryptoDataProviderFactory`) - Selects appropriate provider
3. **Provider Layer** (`BinanceDataProvider`, `CoinGeckoDataProvider`) - Fetches data from APIs
4. **Adapter Layer** (`BinanceAdapter`, `CoinGeckoAdapter`) - Transforms API responses
5. **Model Layer** (`CryptoMarketData`, `CryptoPrice`) - Data transfer objects

## Project Structure

```
CryptoConnectSolution/
├── CryptoConnect/                    # .NET 8 Backend
│   ├── Adapters/                     # API response adapters
│   │   ├── BinanceAdapter.cs
│   │   ├── BinanceHelper.cs
│   │   └── CoinGeckoAdapter.cs
│   ├── DataProviders/                # Provider implementations
│   │   ├── BinanceDataProvider.cs
│   │   └── CoinGeckoDataProvider.cs
│   ├── Factories/                    # Factory pattern implementation
│   │   └── CryptoDataProviderFactory.cs
│   ├── GraphQL/                      # GraphQL schema and resolvers
│   │   ├── Query.cs
│   │   └── CryptoMarketDataType.cs
│   ├── Interfaces/                   # Contracts and abstractions
│   │   ├── ICryptoDataProvider.cs
│   │   ├── ICryptoDataProviderAdapter.cs
│   │   └── ICryptoDataProviderFactory.cs
│   ├── Models/                       # Data models
│   │   ├── CryptoMarketData.cs
│   │   └── CryptoPrice.cs
│   ├── Program.cs                    # Application entry point
│   ├── appsettings.json              # Development configuration
│   └── appsettings.Production.json   # Production configuration
│
└── CryptoConnectUI/                  # React Frontend
    ├── src/
    │   ├── components/               # React components
    │   │   ├── CryptoDashboard.tsx
    │   │   ├── CryptoList/
    │   │   ├── CryptoPrices/
    │   │   ├── CryptoIdSelector/
    │   │   └── ProviderSelector/
    │   ├── services/                 # API communication
    │   │   ├── ApiService.ts
    │   │   └── IGraphQLResponse.ts
    │   ├── interfaces/               # TypeScript interfaces
    │   └── App.tsx
    ├── .env                          # Environment variables
    ├── .env.example                  # Environment template
    └── package.json
```

## Technologies Used

### Backend
- **.NET 8** - Latest LTS version of .NET
- **C# 12** - Modern C# with nullable reference types
- **Hot Chocolate 13** - GraphQL server for .NET
- **ASP.NET Core** - Web framework
- **ILogger** - Structured logging
- **HttpClient** - HTTP communication with external APIs

### Frontend
- **React 18** - UI library
- **TypeScript 5** - Type-safe JavaScript
- **Vite** - Fast build tool and dev server
- **Tailwind CSS 3** - Utility-first CSS framework
- **Axios** - HTTP client
- **ESLint** - Code linting

### DevOps
- **GitHub Actions** - CI/CD pipeline
- **npm** - Package management
- **dotnet CLI** - .NET tooling

## Setup Instructions

### Prerequisites

Ensure you have the following installed:
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 16+** - [Download](https://nodejs.org/)
- **npm** or **yarn** - Comes with Node.js
- **Git** - For cloning the repository

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/iaashu98/CryptoConnectSolution.git
cd CryptoConnectSolution
```

2. **Backend Setup**
```bash
cd CryptoConnect
dotnet restore
dotnet build
```

3. **Frontend Setup**
```bash
cd ../CryptoConnectUI
npm install
```

## Configuration

### Backend Configuration

#### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CryptoProviders": {
    "Binance": {
      "BaseUrl": "https://api.binance.com/",
      "ApiKey": "",
      "ApiSecret": ""
    },
    "CoinGecko": {
      "BaseUrl": "https://api.coingecko.com/api/v3/",
      "ApiKey": ""
    }
  },
  "HttpClient": {
    "TimeoutSeconds": 30
  }
}
```

### Frontend Configuration

#### .env
```bash
VITE_GRAPHQL_ENDPOINT=http://localhost:5136/graphql
```

**Note**: Copy `.env.example` to `.env` and adjust values for your environment.

## Running the Application

### Development Mode

**Terminal 1 - Backend:**
```bash
cd CryptoConnect
dotnet run
```
Backend will start on `http://localhost:5136`

**Terminal 2 - Frontend:**
```bash
cd CryptoConnectUI
npm run dev
```
Frontend will start on `http://localhost:5173`

### Production Build

**Backend:**
```bash
cd CryptoConnect
dotnet publish -c Release
```

**Frontend:**
```bash
cd CryptoConnectUI
npm run build
npm run preview
```

## API Documentation

### GraphQL Endpoint
- **URL**: `http://localhost:5136/graphql`
- **Playground**: Available in development mode

### Sample Queries

#### Fetch Cryptocurrency Prices
```graphql
query {
  cryptoPrices(cryptoIds: ["bitcoin", "ethereum"], provider: "Binance") {
    prices {
      key
      value
    }
  }
}
```

#### Fetch Market Data
```graphql
query {
  cryptoMarketData(cryptoIds: ["bitcoin", "ethereum"], provider: "CoinGecko") {
    id
    name
    symbol
    currentPrice
    marketCap
    volume
  }
}
```

#### Response Format
```json
{
  "data": {
    "cryptoMarketData": [
      {
        "id": "bitcoin",
        "name": "Bitcoin",
        "symbol": "BTC",
        "currentPrice": 42000.50,
        "marketCap": 820000000000,
        "volume": 28000000000
      }
    ]
  }
}
```

## Design Patterns

### 1. Factory Pattern
**Purpose**: Dynamic creation of data provider instances

**Implementation**: `CryptoDataProviderFactory` selects and instantiates the appropriate provider based on user input.

**Benefits**:
- Easy addition of new providers
- Centralized provider management
- Runtime provider selection

### 2. Strategy Pattern
**Purpose**: Interchangeable data retrieval strategies

**Implementation**: Each provider implements `ICryptoDataProvider`, allowing runtime strategy switching.

**Benefits**:
- Flexible provider switching
- Consistent interface
- Easy testing and mocking

### 3. Adapter Pattern
**Purpose**: Normalize different API response formats

**Implementation**: Provider-specific adapters (`BinanceAdapter`, `CoinGeckoAdapter`) transform API responses into a unified format.

**Benefits**:
- Consistent data structure
- Isolated API-specific logic
- Easy API version updates

## Error Handling

### Backend Error Handling

1. **HTTP Status Code Handling**
   - 429 (Rate Limit): User-friendly message with retry suggestion
   - 4xx/5xx: Detailed error logging with context

2. **GraphQL Error Filter**
   - Passes through meaningful error messages
   - Sanitizes sensitive information
   - Structured error responses

3. **Logging**
   - Warning level for rate limits
   - Error level for failures
   - Includes request context (symbols, provider)

### Frontend Error Handling

1. **API Error Handling**
   - Try-catch blocks in all API calls
   - GraphQL error detection
   - Network error handling

2. **User Feedback**
   - Error state display in UI
   - Specific error messages
   - Loading states

## Troubleshooting

### Common Issues

#### 1. CoinGecko Rate Limit Errors

**Symptom**: Error message "CoinGecko API rate limit exceeded"

**Cause**: CoinGecko's free API has strict rate limits (10-50 requests/minute)

**Solutions**:
- Switch to Binance provider (higher rate limits)
- Wait 60 seconds before retrying
- Implement caching (future enhancement)
- Upgrade to CoinGecko Pro API

#### 2. CORS Errors

**Symptom**: "Access-Control-Allow-Origin" error in browser console

**Solution**: Ensure backend CORS is configured correctly in `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    builder => builder.AllowAnyHeader()
                     .AllowAnyMethod()
                     .WithOrigins("http://localhost:5173"));
});
```

#### 3. Port Already in Use

**Backend (5136)**:
```bash
# Find and kill process
lsof -ti:5136 | xargs kill -9
```

**Frontend (5173)**:
```bash
# Find and kill process
lsof -ti:5173 | xargs kill -9
```

#### 4. TypeScript Compilation Errors

**Solution**: Ensure dependencies are installed
```bash
cd CryptoConnectUI
rm -rf node_modules package-lock.json
npm install
```

## Production Considerations

### Security
- [ ] Add API key authentication
- [ ] Implement rate limiting on GraphQL endpoint
- [ ] Enable HTTPS
- [ ] Sanitize user inputs
- [ ] Add CORS whitelist for production domains

### Performance
- [ ] Implement response caching (Redis)
- [ ] Add request throttling/debouncing
- [ ] Enable compression (Gzip/Brotli)
- [ ] Optimize bundle size
- [ ] Add CDN for static assets

### Monitoring
- [ ] Add Application Insights or similar
- [ ] Set up error tracking (Sentry)
- [ ] Configure structured logging
- [ ] Add health check endpoints
- [ ] Monitor API rate limit usage

### Scalability
- [ ] Containerize with Docker
- [ ] Add load balancing
- [ ] Implement horizontal scaling
- [ ] Use managed database for caching
- [ ] Add message queue for async processing

## Future Enhancements

### Short Term
- [ ] Add caching layer (Redis/In-Memory)
- [ ] Implement retry logic with exponential backoff
- [ ] Add more cryptocurrencies
- [ ] Improve error messages
- [ ] Add loading skeletons

### Medium Term
- [ ] Real-time updates via GraphQL subscriptions
- [ ] Historical price charts
- [ ] Price alerts and notifications
- [ ] User authentication and favorites
- [ ] Advanced filtering and sorting

### Long Term
- [ ] Support for additional providers (Kraken, CoinMarketCap, Coinbase)
- [ ] Multi-currency support (EUR, GBP, JPY, INR)
- [ ] Portfolio tracking
- [ ] Trading integration
- [ ] Mobile app (React Native)
- [ ] AI-powered price predictions

## Adding New Providers

To add a new cryptocurrency data provider:

1. **Create Provider Class**
```csharp
public class NewProviderDataProvider : ICryptoDataProvider
{
    public string ProviderName => "NewProvider";
    // Implement interface methods
}
```

2. **Create Adapter Class**
```csharp
public class NewProviderAdapter : ICryptoDataProviderAdapter
{
    // Implement data transformation logic
}
```

3. **Register in DI Container** (`Program.cs`)
```csharp
builder.Services.AddSingleton<ICryptoDataProvider, NewProviderDataProvider>();
```

4. **Add Configuration** (`appsettings.json`)
```json
{
  "CryptoProviders": {
    "NewProvider": {
      "BaseUrl": "https://api.newprovider.com/",
      "ApiKey": ""
    }
  }
}
```

## Contributing

We welcome contributions! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow existing code style
- Add unit tests for new features
- Update documentation
- Ensure CI/CD pipeline passes
- Add meaningful commit messages

## License

This project is open source and available under the [MIT License](LICENSE).

## Acknowledgments

- [Hot Chocolate](https://chillicream.com/docs/hotchocolate) - GraphQL server
- [Binance API](https://binance-docs.github.io/apidocs/) - Cryptocurrency data
- [CoinGecko API](https://www.coingecko.com/en/api) - Cryptocurrency data
- [Tailwind CSS](https://tailwindcss.com/) - UI styling

---

**Every step counts!**
