import { useState, useEffect } from 'react';
import { ICryptoMarketData } from '../interfaces/ICryptoMarketData';
import { fetchCryptoMarketData, fetchCryptoPrices } from '../services/ApiService';
import ProviderSelector from './providerselector/ProviderSelector';
import CryptoIdSelector from './cryptoidselector/CryptoIdSelector';
import CryptoList from './cryptolist/CryptoList';
import CryptoPrices from './cryptoprices/CryptoPrices';
import { ICryptoPrices } from '../interfaces/ICryptoPrices';

const CryptoDashboard = () => {
    const [selectedProvider, setSelectedProvider] = useState<string>('binance');
    const [selectedCryptoIds, setSelectedCryptoIds] = useState<string[]>(['bitcoin']);
    const [marketData, setMarketData] = useState<ICryptoMarketData[]>([]);
    const [marketPrice, setMarketPrice] = useState<ICryptoPrices>();
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            setError(null);

            try {
                // Fetch both market data and prices concurrently
                const [dataResult, priceResult] = await Promise.all([
                    fetchCryptoMarketData(selectedCryptoIds, selectedProvider),
                    fetchCryptoPrices(selectedCryptoIds, selectedProvider)
                ]);

                setMarketData(dataResult);
                setMarketPrice(priceResult);
            } catch (err) {
                const errorMessage = err instanceof Error ? err.message : 'Failed to fetch crypto data';
                setError(errorMessage);
                console.error('Error fetching crypto data:', err);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [selectedProvider, selectedCryptoIds]);

    return (
        <div className="w-full min-w-5xl p-8 bg-gray-900 rounded-lg shadow-xl space-y-6 min-h-[600px]">
            <h1 className="text-3xl font-semibold text-center">Crypto Market Dashboard</h1>

            <ProviderSelector selectedProvider={selectedProvider} onProviderChange={setSelectedProvider} />
            <CryptoIdSelector selectedCryptoIds={selectedCryptoIds} onCryptoIdChange={setSelectedCryptoIds} />

            {error && (
                <div className="bg-red-500 text-white p-4 rounded-lg">
                    <p className="font-semibold">Error:</p>
                    <p>{error}</p>
                </div>
            )}

            <div className="overflow-y-auto max-h-[250px]">
                <CryptoList marketData={marketData} loading={loading} />
                <br />
                <CryptoPrices marketPrice={marketPrice} loading={loading} />
            </div>
        </div>
    );
};

export default CryptoDashboard;