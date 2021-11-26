using System.Threading.Tasks;
using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Repositories
{
    public class SubscribeRepository : BaseRepository<Subscribe>, ISubscribeRepository
    {
        public SubscribeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Subscribe> FindByMinutes(int minutes)
        {
            return await _dbSet.SingleOrDefaultAsync(s => s.Minutes == minutes);
        }
    }
}