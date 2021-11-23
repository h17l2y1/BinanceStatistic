using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;

namespace BinanceStatistic.DAL.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}