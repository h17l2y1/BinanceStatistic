using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;

namespace BinanceStatistic.DAL.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationContext context): base(context)
        {
        }
    }
}