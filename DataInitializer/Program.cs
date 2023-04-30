using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using System.Configuration;

namespace DataInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string? connectionString = ConfigurationManager.AppSettings?["connectionString"];
            if (connectionString == null)
            {
                throw new Exception("Connection string should be passed in the configuration");
            }
            //DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<ExchangeMarketContext>();
            //optionsBuilder.UseMySQL(connectionString);

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<ExchangeMarketContext>(options =>
                        options.UseMySQL(connectionString));
                }).Build();


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var url = @"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year=2023";
                    var context = services.GetRequiredService<ExchangeMarketContext>();
                    DataInitializer.InitializeDbData(context, url).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }

}
