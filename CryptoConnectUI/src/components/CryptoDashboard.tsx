import { useState, useEffect } from 'react';
import { ICryptoMarketData } from '../interfaces/ICryptoMarketData';
import { fetchCryptoMarketData } from '../services/ApiService';
import ProviderSelector from './providerselector/ProviderSelector';
import CryptoIdSelector from './cryptoidselector/CryptoIdSelector';
import CryptoList from './cryptolist/CryptoList';

const CryptoDashboard = () => {
    const [selectedProvider, setSelectedProvider] = useState<string>('binance');
    const [selectedCryptoIds, setSelectedCryptoIds] = useState<string[]>(['bitcoin']);
    const [marketData, setMarketData] = useState<ICryptoMarketData[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            const data = await fetchCryptoMarketData(selectedCryptoIds, selectedProvider);
            setMarketData(data);
            setLoading(false);
        };
        fetchData();
    }, [selectedProvider, selectedCryptoIds]);

    return (
        <div className="w-full min-w-5xl p-8 bg-gray-900 rounded-lg shadow-xl space-y-6 min-h-[600px]">
            <h1 className="text-3xl font-semibold text-center">Crypto Market Dashboard</h1>

            <ProviderSelector selectedProvider={selectedProvider} onProviderChange={setSelectedProvider} />
            <CryptoIdSelector selectedCryptoIds={selectedCryptoIds} onCryptoIdChange={setSelectedCryptoIds} />

            <div className="overflow-y-auto max-h-[250px]">
                <CryptoList marketData={marketData} loading={loading} />
            </div>
        </div>
    );
};

export default CryptoDashboard;