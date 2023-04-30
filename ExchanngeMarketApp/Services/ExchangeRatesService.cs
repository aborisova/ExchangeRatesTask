using Microsoft.EntityFrameworkCore;
using Repository;
namespace ExchanngeMarketApp.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        public ExchangeMarketContext MarketContext { get; }

        public ExchangeRatesService(ExchangeMarketContext marketContext)
        {
            MarketContext = marketContext;
        }
        public ExchangeRate GetExchangeRate(DateTime date, string currencyCode)
        {

            var rate = MarketContext.ExchangeRate
                .Where(er => er.Currency.CurrencyCode == currencyCode && er.Date <= date)
                .OrderBy(er => date - er.Date).Include(c => c.Currency).FirstOrDefault();
            return new ExchangeRate
            {
                CurrencyCode = rate?.Currency.CurrencyCode,
                CurrencyAmount = rate?.Currency.UnitAmount ?? 0,
                Date = rate?.Date ?? DateTime.MinValue,
                Rate = rate?.Price ?? 0
            };
        }

        public List<string> GetCurrencies()
        {
            var currencyCodes = MarketContext.Currency.Select(c => c.CurrencyCode).ToList();
            return currencyCodes;
        }
    }
}