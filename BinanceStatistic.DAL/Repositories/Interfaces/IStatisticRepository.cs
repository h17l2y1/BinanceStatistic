using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface IStatisticRepository: IBaseRepository<Statistic>
    {
        Task<List<Statistic>> GetWithInterval(DateTime lastUpdate, int interval);

        Task<DateTime> GetLastUpdate();
    }
}