using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CurrencyExchangeManager.Services;
using CurrencyExchangeManager.Caching;
using CurrencyExchangeManager.Repositories;

namespace CurrencyExchangeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IExchangeRateCache _exchangeRateCache;
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ExchangeRateController(IExchangeRateService exchangeRateService, IExchangeRateCache exchangeRateCache, IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateService = exchangeRateService;
            _exchangeRateCache = exchangeRateCache;
            _exchangeRateRepository = exchangeRateRepository;
        }

        [HttpGet("convert")]
        public async Task<IActionResult> Convert(string baseCurrency, string targetCurrency, decimal amount)
        {
            decimal? rate = await _exchangeRateCache.GetRateAsync(baseCurrency, targetCurrency);
            if (rate == null)
            {
                rate = await _exchangeRateService.GetExchangeRateAsync(baseCurrency, targetCurrency);
                await _exchangeRateCache.SetRateAsync(baseCurrency, targetCurrency, rate.Value);
                await _exchangeRateRepository.SaveRateAsync(baseCurrency, targetCurrency, rate.Value);
            }

            var convertedAmount = amount * rate.Value;
            return Ok(new { BaseCurrency = baseCurrency, TargetCurrency = targetCurrency, Amount = amount, ConvertedAmount = convertedAmount });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _exchangeRateRepository.GetRateHistoryAsync();
            return Ok(history);
        }
    }
}
