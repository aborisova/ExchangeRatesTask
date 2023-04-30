using Microsoft.AspNetCore.Mvc;
using ExchanngeMarketApp.Services;

namespace ExchanngeMarketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRatesService exchangeRatesService;
        public ExchangeRateController(IExchangeRatesService exchangeRatesService)
        {
            this.exchangeRatesService = exchangeRatesService;
        }

        [HttpGet()]
        public ExchangeRate Get([FromQuery] DateTime date, [FromQuery] string currencyCode)
        {
            return exchangeRatesService.GetExchangeRate(date, currencyCode);
        }
    }
}
