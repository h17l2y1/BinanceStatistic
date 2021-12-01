namespace BinanceStatistic.DAL.Entities
{
    public class Statistic : BaseEntity
    {
        public string CurrencyId { get; set; }
        public int Count { get; set; }
        public int Short { get; set; }
        public int Long { get; set; }
        public decimal Amount { get; set; }

        public virtual Currency Currency { get; set; }
    }
}