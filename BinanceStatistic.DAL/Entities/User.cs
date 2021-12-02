using System.Collections.Generic;

namespace BinanceStatistic.DAL.Entities
{
    public class User : BaseEntity
    {
        public long TelegramId { get; set; }
        public long ChatId { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }

        public ICollection<UserSubscribe> UserSubscribes { get; set; }
    }
}