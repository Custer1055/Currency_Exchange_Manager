namespace CurrencyExchangeManager.Caching
{
    public interface IExchangeRateCache
    {
        Task<decimal?> GetRateAsync(string baseCurrency, string targetCurrency);
        Task SetRateAsync(string baseCurrency, string targetCurrency, decimal rate);
    }
}
