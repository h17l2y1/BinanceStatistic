using System.Collections.Generic;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.Telegram.BLL.Models
{
    public class Test
    {
        public Test(ICollection<UserSubscribe> userSubscribes, bool isSubscribed)
        {
            UserSubscribes = userSubscribes;
            IsCreated = !isSubscribed;
            IsRemoved = isSubscribed;
        }
        
        public ICollection<UserSubscribe> UserSubscribes { get; set; }
        public bool IsCreated { get; set; }
        public bool IsRemoved { get; set; }
    }
}