import axios from 'axios';
import { IGraphQLResponse } from './IGraphQLResponse';
import { ICryptoMarketData } from '../interfaces/ICryptoMarketData';
import { ICryptoPrices } from '../interfaces/ICryptoPrices';

const graphqlEndpoint = import.meta.env.VITE_GRAPHQL_ENDPOINT || "http://localhost:5136/graphql";

export const fetchGraphqlData = async <T>(query: string, variables: object = {}): Promise<T> => {
    try {
        const response = await axios.post<IGraphQLResponse<T>>(graphqlEndpoint, {
            query,
            variables
        });

        if (response.data.errors) {
            throw new Error(`GraphQL Error: ${response.data.errors.map((e: { message: string }) => e.message).join(', ')}`);
        }

        return response.data.data;
    } catch (error: unknown) {
        if (axios.isAxiosError(error)) {
            throw new Error(`Network Error: ${error.message}`);
        }
        throw error;
    }
}

export const fetchCryptoMarketData = async (cryptoIds: string[], provider: string): Promise<ICryptoMarketData[]> => {
    try {
        const query = `
            query {
                cryptoMarketData(cryptoIds: ${JSON.stringify(cryptoIds)}, provider: "${provider}") {
                    id
                    symbol
                    name
                    currentPrice
                    marketCap
                    volume
                }
            }
        `;
        const response = await fetchGraphqlData<{ cryptoMarketData: ICryptoMarketData[] }>(query);
        return response.cryptoMarketData;
    } catch (error) {
        console.error('Failed to fetch crypto market data:', error);
        throw error;
    }
};

// Function to fetch Crypto Prices
export const fetchCryptoPrices = async (cryptoIds: string[], provider: string): Promise<ICryptoPrices> => {
    try {
        const query = `
            query {
                cryptoPrices(cryptoIds: ${JSON.stringify(cryptoIds)}, provider: "${provider}") {
                    prices {
                        key
                        value
                    }
                }
            }
        `;

        const response = await fetchGraphqlData<{ cryptoPrices: ICryptoPrices }>(query);
        return response.cryptoPrices;
    } catch (error) {
        console.error('Failed to fetch crypto prices:', error);
        throw error;
    }
};