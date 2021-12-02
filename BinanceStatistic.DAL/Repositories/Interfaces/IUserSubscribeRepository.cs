using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface IUserSubscribeRepository : IBaseRepository<UserSubscribe>
    {
        Task<List<User>> GetUsersWithIntervalSubscriptions(int minutes);
    }
}