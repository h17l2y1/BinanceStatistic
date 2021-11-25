using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;

namespace BinanceStatistic.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context): base(context)
        {
        }
    }
}