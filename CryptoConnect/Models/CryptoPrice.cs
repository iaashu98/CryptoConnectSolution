namespace CryptoConnect.Models
{
    public class CryptoPrice
    {
        public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
    }
}