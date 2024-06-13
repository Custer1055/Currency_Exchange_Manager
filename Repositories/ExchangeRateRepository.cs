using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurrencyExchangeManager.Data;
using CurrencyExchangeManager.Models;

namespace CurrencyExchangeManager.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly CurrencyExchangeDbContext _context;

        public ExchangeRateRepository(CurrencyExchangeDbContext context)
        {
            _context = context;
        }

        public async Task SaveRateAsync(string baseCurrency, string targetCurrency, decimal rate)
        {
            var exchangeRate = new ExchangeRateHistory
            {
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                Rate = rate,
                Timestamp = DateTime.UtcNow
            };

            _context.ExchangeRateHistories.Add(exchangeRate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ExchangeRateHistory>> GetRateHistoryAsync()
        {
            return await _context.ExchangeRateHistories
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();
        }
    }
}
