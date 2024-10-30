import { IProviderSelector } from "../../interfaces/IProviderSelector";

const providers = ['binance', 'coingecko'];

const ProviderSelector = ({ selectedProvider, onProviderChange }: IProviderSelector) => (
  <div className="mb-4">
    <label className="block text-sm font-medium text-gray-400 mb-2">Select Provider</label>
    <select
      value={selectedProvider}
      onChange={(e) => onProviderChange(e.target.value)}
      className="block w-full px-4 py-2 text-gray-800 rounded-lg bg-white shadow focus:outline-none focus:ring focus:ring-blue-500"
    >
      {providers.map((provider) => (
        <option key={provider} value={provider}>
          {provider.charAt(0).toUpperCase() + provider.slice(1)}
        </option>
      ))}
    </select>
  </div>
);

export default ProviderSelector;