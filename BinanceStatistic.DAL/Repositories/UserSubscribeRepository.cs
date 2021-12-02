using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Repositories
{
    public class UserSubscribeRepository : BaseRepository<UserSubscribe>, IUserSubscribeRepository
    {
        public UserSubscribeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersWithIntervalSubscriptions(int minutes)
        {
            var xxx = await _dbSet
                .Include(i => i.User)
                .Include(i => i.Subscribe)
                .Where(w => w.Subscribe.Minutes == minutes)
                .Select(s => s.User)
                .ToListAsync();

            return xxx;
        }
    }
}