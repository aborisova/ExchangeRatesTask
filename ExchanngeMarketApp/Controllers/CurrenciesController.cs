using Microsoft.AspNetCore.Mvc;
using ExchanngeMarketApp.Services;

namespace ExchanngeMarketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IExchangeRatesService exchangeRatesService;

        public CurrenciesController(IExchangeRatesService exchangeRatesService)
        {
            this.exchangeRatesService = exchangeRatesService;
        }

        [HttpGet()]
        public List<string> Get()
        {
            return exchangeRatesService.GetCurrencies();
        }
    }
}
