using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
// using MySQL.EntityFrameworkCore.Extensions;

namespace Repository
{
    public class ExchangeMarketContext : DbContext
    {
        public ExchangeMarketContext(DbContextOptions<ExchangeMarketContext> options) : base(options)
        {
            
        }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }

        public DbSet<Currency> Currency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.HasIndex(e => e.CurrencyId);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Price);
                entity.HasOne(e => e.Currency).WithMany(e => e.ExchangeRates);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}