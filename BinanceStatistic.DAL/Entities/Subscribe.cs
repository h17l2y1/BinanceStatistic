using System.Collections.Generic;

namespace BinanceStatistic.DAL.Entities
{
    public class Subscribe : BaseEntity
    {
        public Subscribe(string name, int minutes)
        {
            Name = name;
            Minutes = minutes;
        }

        public string Name { get; set; }
        public int Minutes { get; set; }

        public ICollection<UserSubscribe> UserSubscribes { get; set; }
    }
}