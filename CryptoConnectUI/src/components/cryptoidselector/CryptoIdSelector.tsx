import { ICryptoIdSelector } from "../../interfaces/ICryptoIdSelector";

const cryptoOptions = ['bitcoin', 'ethereum', 'shiba', 'litecoin', 'polkadot', 'tron']; 

const CryptoIdSelector = ({ selectedCryptoIds, onCryptoIdChange }: ICryptoIdSelector) => {
  const handleCryptoSelect = (cryptoId: string) => {
    const newCryptoIds = selectedCryptoIds.includes(cryptoId)
      ? selectedCryptoIds.filter((id) => id !== cryptoId)
      : [...selectedCryptoIds, cryptoId];
    onCryptoIdChange(newCryptoIds);
  };

  return (
    <div className="mb-6">
      <label className="block text-sm font-medium text-gray-400 mb-2">Select Cryptocurrencies</label>
      <div className="grid grid-cols-3 gap-2">
        {cryptoOptions.map((crypto) => (
          <button
            key={crypto}
            onClick={() => handleCryptoSelect(crypto)}
            className={`px-4 py-2 rounded-lg text-center transition-colors ${
              selectedCryptoIds.includes(crypto)
                ? 'bg-blue-600 text-white'
                : 'bg-gray-300 text-gray-800'
            } hover:bg-blue-500 focus:outline-none`}
          >
            {crypto.charAt(0).toUpperCase() + crypto.slice(1)}
          </button>
        ))}
      </div>
    </div>
  );
};

export default CryptoIdSelector;