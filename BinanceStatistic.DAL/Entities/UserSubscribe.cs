namespace BinanceStatistic.DAL.Entities
{
    public class UserSubscribe : BaseEntity
    {
        public UserSubscribe(string userId, string subscribeId)
        {
            UserId = userId;
            SubscribeId = subscribeId;
        }
        
        public string UserId { get; set; }
        public string SubscribeId { get; set; }
        
        
        public virtual User User { get; set; }
        public virtual Subscribe Subscribe { get; set; }
    }
}