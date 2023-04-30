using Repository;
using System;
using System.Globalization;

namespace DataInitializer
{
    internal static class Parser
    {

        internal static (List<Currency> currencies, List<RawDataDto> exchangeRates) Parse(string dataString)

        {
            var lines = dataString.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var currencies = ParseCurencies(lines[0]).ToList();
            List<RawDataDto> exchangeRates = lines.Skip(1).Select(l => ParseExchangeRates(l, currencies)).ToList();
            return (currencies, exchangeRates);
        }

        private static Currency ParseCarrency(string currencuString)
        {
            var currency = currencuString.Split();
            return new Currency() { CurrencyCode = currency[1].ToUpperInvariant(), UnitAmount = int.Parse(currency[0]) };
        }

        private static IEnumerable<Currency> ParseCurencies(string currenciesString)
        {
            return currenciesString.Trim().Split('|').Skip(1).Select(ParseCarrency);
        }

        private static RawDataDto ParseExchangeRates(string ratesString, List<Currency> currencies)
        {
            var rates = ratesString.Split('|');
            DateTime date;
            try
            {
                 date = DateTime.Parse(rates[0]);
            }
            catch (Exception)
            {

                throw; // todo
            }
            if (rates.Length != currencies.Count + 1)
            {
                throw new Exception();//add something
            }

            var rateValues = new Dictionary<string, double>();
            for (int i = 1; i < rates.Length; i++)
            {
                rateValues.Add(currencies[i - 1].CurrencyCode, double.Parse(rates[i], CultureInfo.InvariantCulture));
            }

            return new RawDataDto() { Date = date, ExchangeRates = rateValues };
        }
    }
}