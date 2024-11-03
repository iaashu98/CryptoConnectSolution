import axios from 'axios';
import { IGraphQLResponse } from './IGraphQLResponse';
import { ICryptoMarketData } from '../interfaces/ICryptoMarketData';
import { ICryptoPrices } from '../interfaces/ICryptoPrices';

const graphqlEndpoint = "http://localhost:5136/graphql";

export const fetchGraphqlData = async <T>(query: string, variables: object ={}): Promise<T> => {
    const response = await axios.post<IGraphQLResponse<T>>(graphqlEndpoint, {
        query,
        variables
    });

    return response.data.data;
}

export const fetchCryptoMarketData = async (cryptoIds: string[], provider: string): Promise<ICryptoMarketData[]> => {
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
};

// Function to fetch Crypto Prices
export const fetchCryptoPrices = async (cryptoIds: string[], provider: string): Promise<ICryptoPrices> => {
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

    const response = await fetchGraphqlData<{cryptoPrices : ICryptoPrices}>(query);
    return response.cryptoPrices;
};