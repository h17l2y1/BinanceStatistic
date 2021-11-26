using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;

namespace BinanceStatistic.DAL.Repositories
{
    public class UserSubscribeRepository : BaseRepository<UserSubscribe>, IUserSubscribeRepository
    {
        public UserSubscribeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}