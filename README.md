# CryptoConnect Solution

A full-stack crypto dashboard using .NET Core for the backend and React for the frontend, connecting with Binance and CoinGecko APIs to deliver up-to-date crypto market data. The app uses the Adapter pattern for seamless integration of various data sources and the Factory pattern for dynamic provider selection.

## Table of Contents
- [Project Structure](#project-structure)
- [Features](#features)
- [Technologies Used](#tech-stack)
- [Setup Instructions](#setup-instructions)
- [Running the Application](#running-the-application)
- [Design Patterns Used](#design-patterns-used)
- [Future Enhancements](#future-enhancements)
- [GitHub Actions](#github-actions)
- [Adding New Providers](#adding-new-providers)
- [Contributing](#contributing)


## Project Structure
- `CryptoConnect` - .NET Core using GraphQL API to manage and serve crypto data, adapting Binance and CoinGecko APIs.
- `CryptoConnectUI` - React frontend for user interactions with the crypto dashboard.

## Features
1. **Live Market Data** - Fetches and displays latest cryptocurrency data.
2. **Provider Selection** - Chooses data from providers like Binance or CoinGecko.
3. **Responsive UI** - Styled with Tailwind CSS for a modern interface.
4. **Dynamic Provider Selection:** Clients can specify a provider in their GraphQL query, and the application will dynamically switch to that provider to retrieve data.
5. **Standardized Data Format:** Regardless of the selected provider, data is presented in a consistent format with fields like id, name, currentPrice, marketCap, etc.

## Technologies Used
- **Frontend**: React, TypeScript, Tailwind CSS
- **Backend**: .NET 8, C#, GraphQL via Hot Chocolate 
- **Strategy, Adapter,** and **Factory Patterns** for provider selection and data consistency
- **External APIs:** Binance, CoinGecko (additional providers can be added)

## Setup Instructions

### Prerequisites
- .NET 8 SDK 
- Node.js 16+ and npm

### Backend Setup
1. Clone the repository.
```bash
   git clone https://github.com/iaashu98/CryptoConnectSolution.git
```
2. Navigate to `CryptoConnect`
3. Restore the backend: `dotnet restore`
4. Build the backend: `dotnet build`
5. Start the backend: `dotnet run`

### Frontend Setup
1. Navigate to `CryptoConnectUI`
2. Install dependencies: `npm install`
3. Build the frontend: `npm run build`
4. Start the frontend: `npm run dev`

## Running the Application

The backend runs on `http://localhost:5136` and the frontend on `http://localhost:5173`.
Additionally, you can test the backend using Postman with this link `http://localhost:5000/graphql`. 


## Design Patterns Used
### Factory Pattern:
- **Purpose:** The factory is responsible for creating instances of cryptocurrency data providers dynamically based on user-specified providers.
- **Implementation:** CryptoDataProviderFactory selects and instantiates the appropriate data provider class, allowing seamless addition of new providers in the future without extensive code changes.

### Strategy Pattern:
- **Purpose:** To enable switching between different data retrieval strategies at runtime based on the chosen provider.
- **Implementation:** Each data provider (e.g., Binance, CoinGecko) implements the ICryptoDataProvider interface, allowing the application to dynamically use different strategies for fetching and processing cryptocurrency data based on user input.

### Adapter Pattern:
- **Purpose:** To bridge the differences in data structure between external APIs and the application’s standardized data format.
- **Implementation:** Each provider-specific class acts as an adapter (e.g., BinanceAdapter, CoinGeckoAdapter), converting API responses into a consistent format expected by the GraphQL API, ensuring data compatibility and a seamless client experience.

## Future Enhancements
- Extend support for more cryptocurrency data providers like Kraken, CoinMarketCap, and others.
- Integrate a robust error-handling mechanism with retries for transient errors.
- Enable real-time updates for cryptocurrency prices and market data through GraphQL subscriptions.
- Provide advanced filtering options (e.g., by market cap, volume) and complex queries for more detailed data retrieval.
- Enable support for multiple languages and different fiat currencies (e.g. INR, PKR, IDR, BRL, NGN, MXN etc).


## GitHub Actions
CI pipeline to test backend and frontend on each push and pull requests with error notifications.

## Adding New Providers

This project uses the Strategy and Adapter Patterns to support multiple data providers. To add a new provider:

- **Implement the ICryptoDataProvider Interface:** Create a new provider class that implements GetCryptoPricesAsync and GetCryptoMarketDatasAsync.
- **Add the Provider to the Factory:** Update CryptoDataProviderFactory to register the new provider.
- **Configure API Details:** Add any necessary API keys and endpoints for the provider in appsettings.json.

## Contributions
We’re actively looking to expand the capabilities of CryptoConnect. Please review the [Future Enhancements](#future-enhancements) section for ideas, and feel free to contribute by submitting issues, pull requests, or suggestions to further improve this project.