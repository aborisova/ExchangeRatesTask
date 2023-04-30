namespace Repository
{
    public class ExchangeRate
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public Double Price { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }       
    }
}