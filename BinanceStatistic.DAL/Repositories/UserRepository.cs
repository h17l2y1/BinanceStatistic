using System.Threading.Tasks;
using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context): base(context)
        {
        }

        public async Task<User> FindUserByTelegramId(long telegramId)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(s => s.TelegramId == telegramId);
        }
        
        public async Task<User> GetUserWithSubscriptions(long telegramId)
        {
            return await _dbSet.Include(i=>i.UserSubscribes)
                                .ThenInclude(ti=>ti.Subscribe)
                               .SingleOrDefaultAsync(s => s.TelegramId == telegramId);
        }
    }
}