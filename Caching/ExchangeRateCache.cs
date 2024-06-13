using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace CurrencyExchangeManager.Caching
{
    public class ExchangeRateCache : IExchangeRateCache
    {
        private readonly IConnectionMultiplexer _redis;

        public ExchangeRateCache(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<decimal?> GetRateAsync(string baseCurrency, string targetCurrency)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync($"{baseCurrency}_{targetCurrency}");
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            return decimal.Parse(value);
        }

        public async Task SetRateAsync(string baseCurrency, string targetCurrency, decimal rate)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync($"{baseCurrency}_{targetCurrency}", rate, TimeSpan.FromMinutes(15));
        }
    }
}
