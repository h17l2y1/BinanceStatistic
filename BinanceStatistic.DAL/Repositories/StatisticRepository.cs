using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Repositories
{
    public class StatisticRepository : BaseRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(ApplicationContext context) : base(context)
        {
        }
        
        public override async Task<List<Statistic>> GetAll()
        {
            return await _dbSet.AsNoTracking()
                               .Include(i=>i.Currency)
                               .OrderByDescending(o=>o.Count)
                               .ToListAsync();
        }
        
        public async Task<List<Statistic>> GetWithInterval(DateTime lastUpdate, int interval)
        {
            DateTime correctTime = lastUpdate.AddMinutes(-interval);
            
            return await _dbSet.AsNoTracking()
                               .Include(i=>i.Currency)
                               .Where(w=>w.CreationDate >= correctTime)
                               .OrderByDescending(o=>o.Count)
                               .ToListAsync();
        }

        public async Task<DateTime> GetLastUpdate()
        {
            Statistic statistic = await _dbSet.AsNoTracking().OrderByDescending(o => o.CreationDate).FirstOrDefaultAsync();
            return statistic.CreationDate;
        }
    }
}