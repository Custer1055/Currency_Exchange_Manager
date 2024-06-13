using System.Threading.Tasks;

namespace CurrencyExchangeManager.Caching
{
    public interface IExchangeRateCache
    {
        Task SetRateAsync(string baseCurrency, string targetCurrency, decimal rate);
        Task<decimal?> GetRateAsync(string baseCurrency, string targetCurrency);
    }
}
