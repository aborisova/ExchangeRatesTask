namespace Repository
{
    public class Currency
    {
        public int Id { get; set; }

        public string CurrencyCode { get; set; }

        public int UnitAmount { get; set; }

        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}