using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeManager.Models; 

namespace CurrencyExchangeManager.Repositories
{
    public interface IExchangeRateRepository
    {
        Task SaveRateAsync(string baseCurrency, string targetCurrency, decimal rate);
        Task<List<ExchangeRateHistory>> GetRateHistoryAsync();
    }
}
