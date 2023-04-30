namespace ExchanngeMarketApp.Services
{
    public interface IExchangeRatesService
    {
        List<string> GetCurrencies();
        ExchangeRate GetExchangeRate(DateTime date, string currencyCode);
    }
}