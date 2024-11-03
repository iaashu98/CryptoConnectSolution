import { ICryptoPricesList } from "../../interfaces/ICryptoPricesList"
import CustomShimmer from "../CustomShimmer"

const CryptoPrices = ({marketPrice, loading}: ICryptoPricesList) => {

    if(loading) return <div>loading</div>
  return (
    <div className="w-full min-w-full overflow-x-auto bg-gray-800 rounded-lg shadow-md">
        <table className="min-w-full table-auto text-left text-gray-200">
          <thead className="sticky top-0 z-10 bg-gradient-to-r from-indigo-600 to-purple-600 text-white">
            <tr>
              <th className="px-6 py-3">Symbol</th>
              <th className="px-6 py-3">Current Price</th>
            </tr>
          </thead>
          <tbody>
        {loading
          ? Array(5).fill(0).map((_, i) => <CustomShimmer key={i} />)
          : marketPrice?.prices.map((crypto) => (
              <tr
                className="odd:bg-gray-700 even:bg-gray-800 hover:bg-gray-600 transition-colors duration-150"
              >
                <td className="px-6 py-4 border-b border-gray-700">{crypto.key}</td>
                <td className="px-6 py-4 border-b border-gray-700">
                  {crypto.value.toLocaleString("en-US", {
                    style: "currency",
                    currency: "USD",
                  })}
                </td>
              </tr>
            ))}
      </tbody>
        </table>
    </div>
  )
}

export default CryptoPrices