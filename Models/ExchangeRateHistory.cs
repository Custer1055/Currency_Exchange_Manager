namespace CurrencyExchangeManager.Models
{
    public class ExchangeRateHistory
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
