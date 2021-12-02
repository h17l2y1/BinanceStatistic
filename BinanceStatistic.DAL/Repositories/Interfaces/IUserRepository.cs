using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindUserByTelegramId(long telegramId);

        Task<User> GetUserWithSubscriptions(long telegramId);

    }
}