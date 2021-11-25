namespace BinanceStatistic.DAL.Entities
{
    public class User : BaseEntity
    {
        public string TelegramId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
    }
}