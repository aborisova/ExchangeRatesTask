using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Org.BouncyCastle.Asn1.Pkcs;
using Repository;

namespace DataInitializer
{
    internal static class DataInitializer
    {
        internal static async Task InitializeDbData(ExchangeMarketContext dbContext, string sourceUrl)
        {
            var rawData = await GetData(sourceUrl);
            await InsertData(dbContext, rawData.currencies, rawData.exchangeRates);
        }
        private static async Task<(List<Currency> currencies, List<RawDataDto> exchangeRates)> GetData(string sourceUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var dataString = await httpClient.GetStringAsync(sourceUrl);

                (List<Currency> currencies, List<RawDataDto> exchangeRates) rawData = Parser.Parse(dataString);

                return rawData;
            }
        }

        private static async Task InsertData(ExchangeMarketContext context, List<Currency> currencies, List<RawDataDto> exchangeRates)
        {
            await context.Database.EnsureCreatedAsync();
            for (int i = 0; i < currencies.Count; i++)
            {
                Currency currency = currencies[i];
                Currency currencyInDb = await context.Currency.SingleOrDefaultAsync(c => c.CurrencyCode == currency.CurrencyCode && c.UnitAmount == currency.UnitAmount);
                if (currencyInDb == null)
                {
                    context.Currency.Add(currency);
                }
                else
                {
                    currencies[i] = currencyInDb;
                }
            }

            await context.SaveChangesAsync();

            foreach (var ratesRow in exchangeRates)
            {
                foreach (var rate in ratesRow.ExchangeRates)
                {
                    if (! await context.ExchangeRate.AnyAsync(er => er.Date == ratesRow.Date && er.Currency.CurrencyCode == rate.Key))
                    {
                        context.ExchangeRate.Add(
                            new ExchangeRate
                            {
                                Date = ratesRow.Date,
                                Price = rate.Value,
                                Currency = currencies.FirstOrDefault(c => c.CurrencyCode == rate.Key)
                            });
                    }
                }
            }
            await context.SaveChangesAsync();
        }
    }
}