const CustomShimmer = () => (
    <tr className="animate-pulse">
    {Array(6).fill(0).map((_, i) => (
      <td key={i} className="px-6 py-4 border-b border-gray-700">
        <div className="h-4 bg-gray-500 rounded w-3/4 mx-auto"></div>
      </td>
    ))}
  </tr>
);

export default CustomShimmer;