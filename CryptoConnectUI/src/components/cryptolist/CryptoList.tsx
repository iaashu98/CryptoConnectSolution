import { ICryptoList } from "../../interfaces/ICryptoList";

const CryptoList = ({ marketData, loading }: ICryptoList) => {

  return (
    <div className="w-full min-w-full overflow-x-auto bg-gray-800 rounded-lg shadow-md">
      {loading ? (
        <div className="min-w-full text-center text-white py-6">Loading...</div>
      ) : (
        <table className="min-w-full table-auto text-left text-gray-200">
          <thead className="sticky top-0 z-10 bg-gradient-to-r from-indigo-600 to-purple-600 text-white">
            <tr>
              <th className="px-6 py-3">ID</th>
              <th className="px-6 py-3">Symbol</th>
              <th className="px-6 py-3">Name</th>
              <th className="px-6 py-3">Current Price</th>
              <th className="px-6 py-3">Market Cap</th>
              <th className="px-6 py-3">Volume</th>
            </tr>
          </thead>
          <tbody>
            {marketData.map((crypto) => (
              <tr
                key={crypto.id}
                className="odd:bg-gray-700 even:bg-gray-800 hover:bg-gray-600 transition-colors duration-150"
              >
                <td className="px-6 py-4 border-b border-gray-700">{crypto.id}</td>
                <td className="px-6 py-4 border-b border-gray-700">{crypto.symbol}</td>
                <td className="px-6 py-4 border-b border-gray-700">{crypto.name}</td>
                <td className="px-6 py-4 border-b border-gray-700">{crypto.currentPrice.toLocaleString("en-US", { style: "currency", currency: "USD" })}</td>
                <td className="px-6 py-4 border-b border-gray-700">{crypto.marketCap.toLocaleString("en-US", { style: "currency", currency: "USD" })}</td>
                <td className="px-6 py-4 border-b border-gray-700">{crypto.volume.toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default CryptoList;