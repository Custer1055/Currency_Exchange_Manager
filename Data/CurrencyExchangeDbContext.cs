using Microsoft.EntityFrameworkCore;
using CurrencyExchangeManager.Models;

namespace CurrencyExchangeManager.Data
{
    public class CurrencyExchangeDbContext : DbContext
    {
        public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options) : base(options)
        {
        }

        public DbSet<ExchangeRateHistory> ExchangeRateHistories { get; set; }
    }
}
