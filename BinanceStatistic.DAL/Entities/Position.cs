namespace BinanceStatistic.DAL.Entities
{
    public class Position : BaseEntity
    {
        public string PositionName { get; set; }
        public int Count { get; set; }
        public int Short { get; set; }
        public int Long { get; set; }
        public decimal Amount { get; set; }
    }
}