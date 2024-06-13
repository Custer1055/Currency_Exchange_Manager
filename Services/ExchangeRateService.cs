using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace CurrencyExchangeManager.Caching
{
    public class ExchangeRateCache : IExchangeRateCache
    {
        private readonly IDatabase _database;

        public ExchangeRateCache(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task SetRateAsync(string baseCurrency, string targetCurrency, decimal rate)
        {
            string cacheKey = $"{baseCurrency}_{targetCurrency}";
            string cacheValue = rate.ToString(); // Convert decimal to string
            await _database.StringSetAsync(cacheKey, cacheValue, TimeSpan.FromMinutes(15));
        }

        public async Task<decimal?> GetRateAsync(string baseCurrency, string targetCurrency)
        {
            string cacheKey = $"{baseCurrency}_{targetCurrency}";
            RedisValue cacheValue = await _database.StringGetAsync(cacheKey);

            if (cacheValue.HasValue)
            {
                if (decimal.TryParse(cacheValue, out decimal rate))
                {
                    return rate;
                }
            }

            return null;
        }
    }
}
