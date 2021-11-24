using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface IPositionRepository: IBaseRepository<Position>
    {
        Task<List<Position>> GetWithInterval(DateTime lastUpdate, int interval);

        Task<DateTime> GetLastUpdate();
    }
}